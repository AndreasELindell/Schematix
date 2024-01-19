using Schematix.Core.DTOs;
using Schematix.Core.Entities;

namespace Schematix.Core.Mappers;

public interface IShiftMapper
{
    IEnumerable<Shift> MapShiftsDto(IEnumerable<ShiftDto> shiftDtos);
    IEnumerable<ShiftDto> MapShifts(IEnumerable<Shift> shifts);

    Shift MapShiftDto(ShiftDto shiftDto);
    ShiftDto MapShift(Shift shift);
}
public class ShiftMapper : IShiftMapper
{
    public ShiftDto MapShift(Shift shift)
    {
        return new ShiftDto
        {
            Id = shift.Id,
            Start = shift.Start,
            End = shift.End,
            Length = shift.Length,
            Date = shift.Date,
            Branch = shift.Branch,
            Employee = shift.Employee
        };
    }

    public IEnumerable<ShiftDto> MapShifts(IEnumerable<Shift> shifts)
    {
        return shifts.Select(MapShift);
    }

    public IEnumerable<Shift> MapShiftsDto(IEnumerable<ShiftDto> shiftDtos)
    {
        return shiftDtos.Select(MapShiftDto);
    }

    public Shift MapShiftDto(ShiftDto shiftDto)
    {
        return new Shift
        {
            Id = shiftDto.Id,
            Start = shiftDto.Start,
            End = shiftDto.End,
            Length = shiftDto.Length,
            Date = shiftDto.Date,
            Branch = shiftDto.Branch,
            Employee = shiftDto.Employee
        };
    }
}
