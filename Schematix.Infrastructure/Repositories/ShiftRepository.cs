using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Schematix.Core.Entities;
using Schematix.Core.Interfaces;
using Schematix.Infrastructure.Context;
using System.Linq;

namespace Schematix.Infrastructure.Repositories;

public class ShiftRepository : IShiftRepository
{
    private readonly DataContext _dataContext;

    public ShiftRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task AddShift(Shift shift)
    {
        _dataContext.Shifts.Add(shift);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteShift(Shift shift)
    {
        _dataContext.Shifts.Remove(shift);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<bool> DoShiftExist(int shiftId)
    {
        return await _dataContext.Shifts.AnyAsync(s => s.Id == shiftId);
    }

    public async Task<Shift> GetShift(Shift shift)
    {
        return (await _dataContext.Shifts.FindAsync(shift))!;
    }

    public async Task<IEnumerable<Shift>> GetShiftsForBranch(int branchId)
    {
        return await _dataContext.Shifts.Where(s => s.BranchId == branchId).ToListAsync();
    }

    public async Task<IEnumerable<Shift>> GetShiftsForEmployee(string employeeId)
    {
        return await _dataContext.Shifts.Where(s => s.EmployeeId == employeeId).ToListAsync();
    }

    public async Task UpdateShift(Shift shift)
    {
        _dataContext.Shifts.Update(shift);
        await _dataContext.SaveChangesAsync();
    }
}
