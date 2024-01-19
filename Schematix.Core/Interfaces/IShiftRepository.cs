﻿using Schematix.Core.Entities;

namespace Schematix.Core.Interfaces;

public interface IShiftRepository
{
    Task<IEnumerable<Shift>> GetShiftsForEmployee(string employeeId);
    Task<IEnumerable<Shift>> GetShiftsForBranch(int branchId);
    Task<Shift> GetShift(Shift shift);
    Task AddShift(Shift shift);
    Task DeleteShift(int shiftId);
    Task UpdateShift(Shift shift);
    Task<bool> DoShiftExist(int shiftId);
}
