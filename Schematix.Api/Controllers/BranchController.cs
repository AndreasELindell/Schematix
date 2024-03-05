using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Schematix.Core.DTOs;
using Schematix.Core.Entities;
using Schematix.Core.Interfaces;
using Schematix.Core.Mappers;

namespace Schematix.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BranchController : ControllerBase
{
    private readonly IBranchRepository _branchRepository;
    private readonly IBranchMapper _mapper;
    private readonly IUserRepository _userRepository;

    public BranchController(IBranchRepository branchRepository, IBranchMapper mapper, IUserRepository userRepository)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<BranchDto>>> GetAllBranches()
    {
        var branches = await _branchRepository.GetAllBranches();

        return Ok(_mapper.MapBranches(branches));
    }
    [HttpGet]
    [Route("{branchId}", Name = "GetBranchById")]
    public async Task<ActionResult<BranchDto>> GetBranchById(int branchId)
    {
        var branch = await _branchRepository.GetBranchByIdWithEmployees(branchId);

        if (branch == null)
        {
            return NotFound();
        }

        var branchDTo = _mapper.MapBranch(branch);

        return Ok(branchDTo);
    }

    [HttpPost]
    public async Task<ActionResult> AddBranch(BranchDto branchDto)
    {
        var branch = _mapper.MapBranchDto(branchDto);

        await _branchRepository.AddBranch(branch);

        return CreatedAtRoute("GetBranchById", new { id = branch.Id }, branch);
    }
    [HttpPost("{branchId}")]
    public async Task<ActionResult> AddEmployeeToBranch(List<string> employeeIds, int branchId)
    {
        if (!await _branchRepository.BranchExist(branchId))
        {
            return NotFound(new { Message = "Branch Not Found" });
        }
        foreach (var employeeid  in employeeIds) 
        {
            if (!await _userRepository.EmployeeExists(employeeid))
            {
                return NotFound(new { Message = "Employee Not Found" });
            }
            var employee = await _userRepository.GetEmployeeById(employeeid);

            await _branchRepository.AddEmployeeToBranch(employee, branchId);
        }

        return Ok(new { Message = $"Employees were added to Branch {branchId}" });
    }
    [HttpDelete("{branchId}/{employeeId}")]
    public async Task<ActionResult> RemoveEmployeeFromBranch(string employeeId, int branchId)
    {
        if (!await _branchRepository.BranchExist(branchId))
        {
            return NotFound(new { Message = "Branch Not Found" });
        }

        if (!await _userRepository.EmployeeExists(employeeId))
        {
            return NotFound(new { Message = "Employee Not Found" });
        }

        var employee = await _userRepository.GetEmployeeById(employeeId);

        await _branchRepository.RemoveEmployeeFromBranch(employee, branchId);

        return Ok(new { Message = $"Employee {employeeId} was removed from Branch {branchId}" });
    }
    [HttpPatch("{branchId}")]
    public async Task<ActionResult> UpdateBranch(int branchId, JsonPatchDocument<Branch> document) 
    { 
        if (!await _branchRepository.BranchExist(branchId)) 
        {
            return NotFound(new { Message = "Branch Not Found" });
        }

        var branch = await _branchRepository.GetBranchByIdWithoutEmployees(branchId);

        document.ApplyTo(branch);

        await _branchRepository.UpdateBranch(branch);

        return Ok(new { Message = $"{branch.Name} manager was updated"});
    }
}
