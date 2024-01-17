using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<List<Employee>>> GetAllEmployees() 
        { 
            var employees = await _userRepository.GetEmployees();

            return Ok(employees);
        }

        [HttpGet("{employeeId}", Name = "GetEmployeeById")]
        public async Task<ActionResult<Employee>> GetEmployeeById(string employeeId)
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
