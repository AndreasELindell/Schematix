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
        return await _dataContext.Shifts
            .Include(s => s.Branch)
            .Include(s => s.Employee)
            .Include(s => s.Tasks)
            .Where(s => s.BranchId == branchId).ToListAsync();
    }

    public async Task<IEnumerable<Shift>> GetShiftsForEmployee(string employeeId, int week)
    {
            DateTime jan1 = new DateTime(DateTime.Now.Year, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);

            DateTime targetDate = firstMonday.AddDays((week - 1) * 7);

            // Calculate the start and end date of the week
            DateOnly startDate = DateOnly.FromDateTime(targetDate);
            DateOnly endDate = startDate.AddDays(6);

            return await _dataContext.Shifts
            .Include(s => s.Branch)
            .Include(s => s.Employee)
            .Include(s => s.Tasks)
            .Where(s => s.EmployeeId == employeeId && s.Date >= startDate && s.Date <= endDate)
            .ToListAsync();
    }

    public async Task UpdateShift(Shift shift)
    {
        _dataContext.Shifts.Update(shift);
        await _dataContext.SaveChangesAsync();
    }
}
