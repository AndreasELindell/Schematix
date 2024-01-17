using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Schematix.Core.DTOs;
using Schematix.Core.Entities;
using Schematix.Core.Interfaces;
using Schematix.Core.Mappers;

namespace Schematix.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeMapper _mapper;

        public UserController(IUserRepository userRepository, IEmployeeMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public IEmployeeMapper Mapper { get; }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> GetAllEmployees() 
        { 
            var employees = await _userRepository.GetEmployees();

            return Ok(_mapper.MapEmployees(employees));
        }

        [HttpGet("{employeeId}", Name = "GetEmployeeById")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(string employeeId)
        {
            var employee = await _userRepository.GetEmployeeById(employeeId);

            if(employee == null) 
            { 
                return NotFound();
            }
            return Ok(_mapper.MapEmployee(employee));
        }

        [HttpGet("Branch/{branchId}")]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployeesFromBranch(int branchId) 
        { 
            var employeesFromBranch = await _userRepository.GetEmployeesFromBranch(branchId);
            
            if(employeesFromBranch == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.MapEmployees(employeesFromBranch));
        }
        [HttpPatch("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee(string employeeId, JsonPatchDocument<EmployeeDto> document, string roleName)
        {
            if(document == null || employeeId == "0" || employeeId is null) 
            { 
                return BadRequest();
            }

            if(!await _userRepository.EmployeeExists(employeeId)) 
            {
                return NotFound();
            }

            var employeeDto = _mapper.MapEmployee(await _userRepository.GetEmployeeById(employeeId));

            document.ApplyTo(employeeDto);

            var employeeToUpdate = _mapper.MapEmployeeDto(employeeDto);

            ReturnEmployeePatchChangesDto ReturnEmployeePatchChangesDto = new();

            var changes = ReturnEmployeePatchChangesDto;

            var respones = new
            {

            };

            return Ok(new {Message = $"Employee{employeeToUpdate.FirstName} {employeeToUpdate.LastName} has been updated"});
        }
    }
}
