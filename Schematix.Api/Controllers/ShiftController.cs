﻿using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Schematix.Core.DTOs;
using Schematix.Core.Entities;
using Schematix.Core.Interfaces;
using Schematix.Core.Mappers;
using Schematix.Infrastructure.Repositories;

namespace Schematix.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IShiftRepository _shiftRepository;
    private readonly IShiftMapper _mapper;
    private readonly IEmployeeMapper _employeeMapper;
    private readonly IBranchRepository _branchRepository;

    public ShiftController(
        IBranchRepository branchRepository,
        IUserRepository userRepository,
        IShiftRepository shiftRepository,
        IShiftMapper mapper,
        IEmployeeMapper employeeMapper)
    {
        _branchRepository = branchRepository;
        _userRepository = userRepository;
        _shiftRepository = shiftRepository;
        _mapper = mapper;
        _employeeMapper = employeeMapper;
    }
    [HttpGet]
    public async Task<ActionResult<List<EmployeeWithShiftsDto>>> GetEmployeesWithShifts(int week)
    {
        List<EmployeeWithShiftsDto> employeesWithShifts = new List<EmployeeWithShiftsDto>();

        var employees = await _userRepository.GetEmployees();

        foreach (var employee in employees) 
        {
            var employeeWithShifts = new EmployeeWithShiftsDto()
            {
                Employee = _employeeMapper.MapEmployee(employee),
                Shifts = _mapper.MapShifts(await _shiftRepository.GetShiftsForEmployee(employee.Id, week))
            };
            employeesWithShifts.Add(employeeWithShifts);
        }

        return employeesWithShifts;
    }

    [HttpGet("branch/{branchId}")]
    public async Task<ActionResult<List<ShiftDto>>> GetShiftsFromBranch(int branchId) 
    { 
        if(!await _branchRepository.BranchExist(branchId))
        {
            return NotFound(    );
        }

        var shifts = await _shiftRepository.GetShiftsForBranch(branchId);

        var shiftsToReturn = _mapper.MapShifts(shifts);

        return Ok(shiftsToReturn);
    }

    [HttpGet("user/{employeeId}")]
    public async Task<ActionResult<List<ShiftDto>>> GetShiftsForEmployee(string employeeId, int week)
    {
        if(!await _userRepository.EmployeeExists(employeeId)) 
        {
            return NotFound();
        }

        var shifts = await _shiftRepository.GetShiftsForEmployee(employeeId, week);

        var shiftsToReturn = _mapper.MapShifts(shifts);

        return Ok(shiftsToReturn);
    }
    [HttpPost]
    public async Task<ActionResult> AddShift(ShiftDto shiftDto) 
    {
        if (!await _userRepository.EmployeeExists(shiftDto.Employee.Id))
        {
            return NotFound();
        }
        if (!await _branchRepository.BranchExist(shiftDto.Branch.Id))
        {
            return NotFound();
        }

        var shift = _mapper.MapShiftDto(shiftDto);
        await _shiftRepository.AddShift(shift);

        return Created();

    }

}
