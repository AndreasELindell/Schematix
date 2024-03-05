using Microsoft.AspNetCore.Identity;
using Schematix.Core.DTOs;
using Schematix.Core.Entities;
using Schematix.Core.Interfaces;
using Schematix.Core.Mappers;

namespace Schematix.Core.Services;
public class EmployeeRoleService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<Employee> _userManager;
    private readonly IEmployeeMapper _mapper;

    public EmployeeRoleService(IUserRepository userRepository, IEmployeeMapper mapper, UserManager<Employee> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<List<EmployeeDto>?> MapRolesToEmployees(IEnumerable<Employee> employees) 
    { 
        
        if(employees == null) 
        {
            return null;
        }

        var employeeDTOs = new List<EmployeeDto>();

        foreach (var employee in employees)
        {
            var employeeDto = _mapper.MapEmployee(employee);

            var e = await _userRepository.GetEmployeeById(employee.Id);
            var roles = await _userManager.GetRolesAsync(e);

            employeeDto.Roles = roles;

            employeeDTOs.Add(employeeDto);
        }
        
        return employeeDTOs;

    }
}
