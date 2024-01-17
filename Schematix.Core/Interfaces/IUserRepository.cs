using Schematix.Core.Entities;

namespace Schematix.Core.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<Employee>> GetEmployees();
    Task<IEnumerable<Employee>> GetEmployeesFromBranch(int branchId);
    Task<Employee> GetEmployeeById(string employeeId);
    Task UpdateEmployee(Employee employee, string? roleName);
    Task DeleteEmployeeById(Employee employee);
    Task<bool> EmployeeExists(string employeeId);
}
