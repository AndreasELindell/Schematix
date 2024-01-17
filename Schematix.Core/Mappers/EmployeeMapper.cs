using Schematix.Core.DTOs;
using Schematix.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schematix.Core.Mappers;

public interface IEmployeeMapper
{
    IEnumerable<EmployeeDto> MapEmployees(IEnumerable<Employee> employees);
    IEnumerable<Employee> MapEmployeesDto(IEnumerable<EmployeeDto> employeesDto);

    Employee MapEmployeeDto(EmployeeDto dto);
    EmployeeDto MapEmployee(Employee employee);

    public class EmployeeMapper : IEmployeeMapper
    {
        public IEnumerable<EmployeeDto> MapEmployees(IEnumerable<Employee> employees)
        {
            return employees.Select(MapEmployee);
        }

        public EmployeeDto MapEmployee(Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                UserName = employee.UserName!,
                Salary = employee.Salary,
                Email = employee.Email!,
                PhoneNumber = employee.PhoneNumber!
            };
        }

        public IEnumerable<Employee> MapEmployeesDto(IEnumerable<EmployeeDto> employeesDto)
        {
            return employeesDto.Select(MapEmployeeDto);
        }

        public Employee MapEmployeeDto(EmployeeDto dto)
        {
            return new Employee
            {
                Id = dto.Id,
                UserName = dto.UserName,
                Salary = dto.Salary,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
            };
        }
    }
}