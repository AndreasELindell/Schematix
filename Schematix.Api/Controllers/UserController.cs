using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schematix.Core.Entities;
using Schematix.Core.Interfaces;

namespace Schematix.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees() 
        { 
            var employees = await _userRepository.GetEmployees();

            return Ok(employees);
        }

        [HttpGet("{employeeId}", Name = "GetEmployeeById")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int employeeId)
        {
            var employee = await _userRepository.GetEmployeeById(employeeId);

            if(employee == null) 
            { 
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpGet("Branch/{branchId}")]
        public async Task<ActionResult<List<Employee>>> GetEmployeesFromBranch(int branchId) 
        { 
            var employeesFromBranch = await _userRepository.GetEmployeesFromBranch(branchId);
            
            if(employeesFromBranch == null) 
            {
                return NotFound();
            }

            return Ok(employeesFromBranch);
        }
        
    }
}
