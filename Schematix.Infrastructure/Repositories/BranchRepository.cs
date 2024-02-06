using Microsoft.EntityFrameworkCore;
using Schematix.Core.Entities;
using Schematix.Core.Interfaces;
using Schematix.Infrastructure.Context;

namespace Schematix.Infrastructure.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly DataContext _dataContext;

    public BranchRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task AddBranch(Branch branch)
    {
        branch.Employees = new List<Employee>();

        await _dataContext.Branches.AddAsync(branch);
        await _dataContext.SaveChangesAsync();
    }

    public async Task AddEmployeeToBranch(Employee employee, int branchId)
    {
        var branch = await GetBranchByIdWithEmployees(branchId);
        if (branch.Employees == null)
        {
            branch.Employees = new List<Employee>();
        }

        branch!.Employees.Add(employee);
        await _dataContext.SaveChangesAsync();
    }

    public Task<bool> BranchExist(int branchId)
    {
        return _dataContext.Branches.AnyAsync(b => b.Id == branchId);
    }

    public async Task DeleteBranch(Branch branch)
    {
        _dataContext.Branches.Remove(branch);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Branch>> GetAllBranches()
    {
        return await _dataContext.Branches
            .Include(b => b.Manager)
            .Include(b => b.Employees)
            .ToListAsync();
    }

    public async Task<Branch?> GetBranchByIdWithEmployees(int branchId)
    {
        return await _dataContext.Branches.Include(b => b.Employees).FirstOrDefaultAsync(b => b.Id == branchId);
    }
    public async Task<Branch?> GetBranchByIdWithoutEmployees(int branchId)
    {
        return await _dataContext.Branches.FirstOrDefaultAsync(b => b.Id == branchId);
    }

    public async Task RemoveEmployeeFromBranch(Employee employee, int branchId)
    {
        var branch = await GetBranchByIdWithEmployees(branchId);
        branch!.Employees.Remove(employee);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateBranch(Branch branch)
    {
        _dataContext.Branches.Update(branch);
        await _dataContext.SaveChangesAsync();
    }
}
