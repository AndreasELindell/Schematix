﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Schematix.Core.Entities;
using Schematix.Core.Interfaces;
using Schematix.Infrastructure.Context;

namespace Schematix.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;
    private readonly UserManager<Employee> _userManager;
    public UserRepository(
        DataContext dataContext,
        UserManager<Employee> userManager
        )
    {
        _dataContext = dataContext;
        _userManager = userManager;
    }

    public async Task DeleteEmployeeById(Employee employee)
    {
        await _userManager.DeleteAsync(employee);
    }

    public async Task<Employee?> GetEmployeeById(string employeeId)
    {
        var user = await _userManager.FindByIdAsync(employeeId);

        if (user == null)
        {
            return null;
        }
        return user;
    }

    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        var users = await _userManager.Users.ToListAsync();
        if (users.Any())
        {
            return users;
        }
        return Enumerable.Empty<Employee>();
    }

    public async Task<IEnumerable<Employee>> GetEmployeesFromBranch(int branchId)
    {
        var branch = await _dataContext.Branches.Include(b => b.Employees).FirstOrDefaultAsync(b => b.Id == branchId);

        if (branch is null || branch.Employees is null)
        {
            return Enumerable.Empty<Employee>();
        }
        return branch.Employees;
    }

    public async Task UpdateEmployee(Employee employee, string? roleName)
    {
        await _userManager.UpdateAsync(employee);
        if (roleName != null)
        {
            await _userManager.AddToRoleAsync(employee, roleName);
        }
    }

    public async Task<bool> EmployeeExists(string employeeId)
    {
        return await _dataContext.Employees.AsNoTracking().AnyAsync(e => e.Id == employeeId);
    }
}
