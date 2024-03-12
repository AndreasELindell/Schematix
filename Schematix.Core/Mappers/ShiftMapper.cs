using Schematix.Core.DTOs;
using Schematix.Core.Entities;
using Schematix.Core.Enums;

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
    private readonly IBranchMapper _branchMapper;
    private readonly IEmployeeMapper _employeeMapper;
    private readonly IWorkTaskMapper _workTaskMapper;

    public ShiftMapper(
        IBranchMapper branchMapper, 
        IEmployeeMapper employeeMapper, 
        IWorkTaskMapper workTaskMapper)
    {
        _branchMapper = branchMapper;
        _employeeMapper = employeeMapper;
        _workTaskMapper = workTaskMapper;
    }
    public ShiftDto MapShift(Shift shift)
    {
        return new ShiftDto
        {
            Id = shift.Id,
            Start = shift.Start,
            End = shift.End,
            Length = shift.Length,
            Date = shift.Date,
            Type = shift.Type.ToString(),
            Branch = _branchMapper.MapBranch(shift.Branch),
            Employee = _employeeMapper.MapEmployee(shift.Employee),
            Tasks = _workTaskMapper.MapWorkTasks(shift.Tasks),
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
        var BranchId = shiftDto.Branch.Id;
        var EmployeeId = shiftDto.Employee.Id;

        ShiftType type;

        var newtype = Enum.TryParse(shiftDto.Type, out type);

        if (Enum.TryParse(shiftDto.Type, true, out type))
        {
            int intValue = (int)type;
        }

        return new Shift
        {
            Id = shiftDto.Id,
            Start = shiftDto.Start,
            End = shiftDto.End,
            Length = shiftDto.Length,
            Date = shiftDto.Date,
            BranchId = BranchId,
            Type = type,
            EmployeeId = EmployeeId,
            Tasks = _workTaskMapper.MapWorkTasksDto(shiftDto.Tasks).ToList(),
        };
    }
}
