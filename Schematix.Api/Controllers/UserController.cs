using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Schematix.Core.DTOs;
using Schematix.Core.Entities;
using Schematix.Core.Interfaces;
using Schematix.Core.Mappers;
using Schematix.Core.Services;

namespace Schematix.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeMapper _mapper;
        private readonly EmployeeRoleService _roleService;

        public UserController(
            IUserRepository userRepository, 
            IEmployeeMapper mapper, 
            EmployeeRoleService roleService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleService = roleService;
        }

        public IEmployeeMapper Mapper { get; }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> GetAllEmployees()
        {
            var employees = await _userRepository.GetEmployees();

            //var employeeDto = _mapper.MapEmployees(employees);
            var employeesWithRole =  await _roleService.MapRolesToEmployees(employees);

            return Ok(employeesWithRole);
        }
        [HttpGet("roles")]
        public async Task<ActionResult> GetAllRoles() 
        { 
            var identityroles = await _userRepository.GetRoles();

            var roles = identityroles.Select(x => x.Name);


            return Ok(roles);
        }

        [HttpGet("{employeeId}", Name = "GetEmployeeById")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(string employeeId)
        {
            var employee = await _userRepository.GetEmployeeById(employeeId);

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(_mapper.MapEmployee(employee));
        }

        [HttpGet("Branch/{branchId}")]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployeesFromBranch(int branchId)
        {
            var employeesFromBranch = await _userRepository.GetEmployeesFromBranch(branchId);

            if (employeesFromBranch == null)
            {
                return NotFound();
            }

            return Ok(_mapper.MapEmployees(employeesFromBranch));
        }
        [HttpPatch("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee(string employeeId, JsonPatchDocument<Employee> document, string? roleName)
        {
            if (document == null || employeeId == "0" || employeeId is null)
            {
                return BadRequest();
            }

            if (!await _userRepository.EmployeeExists(employeeId))
            {
                return NotFound();
            }

            var employee = await _userRepository.GetEmployeeById(employeeId);

            var originalEmployeeToReturn = new Employee
            {
                Id = employee.Id,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                UserName = employee.UserName,
                Salary = employee.Salary
            };

            document.ApplyTo(employee);

            await _userRepository.UpdateEmployee(employee, roleName);

            var response = new
            {
                Changes = ReturnEmployeePatchChangesDto.GetChanges(originalEmployeeToReturn, employee)
            };

            return Ok(response);
        }
    }
}
