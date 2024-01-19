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
    private readonly IUserRepository _userRepository;
    private readonly IBranchRepository _branchRepository;

    public ShiftRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public Task AddShift(Shift shift)
    {
        throw new NotImplementedException();
    }

    public Task DeleteShift(int shiftId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DoShiftExist(int shiftId)
    {
        throw new NotImplementedException();
    }

    public Task<Shift> GetShift(Shift shift)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Shift>> GetShiftsForBranch(int branchId)
    {
        return await _dataContext.Shifts.Where(s => s.BranchId == branchId).ToListAsync();
    }

    public async Task<IEnumerable<Shift>> GetShiftsForEmployee(string employeeId)
    {
        return await _dataContext.Shifts.Where(s => s.EmployeeId == employeeId).ToListAsync();
    }

    public Task UpdateShift(Shift shift)
    {
        throw new NotImplementedException();
    }
}
