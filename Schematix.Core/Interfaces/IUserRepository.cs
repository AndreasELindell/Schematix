using Schematix.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schematix.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<IEnumerable<Employee>> GetEmployeesFromBranch(int branchId);
        Task<Employee> GetEmployeeById(int employeeId);
        Task UpdateEmployee(Employee employee, string? roleName);
        Task DeleteEmployeeById(Employee employee);
        Task<bool> EmployeeExists(Employee employee);
    }
}
