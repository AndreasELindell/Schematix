using Schematix.Core.DTOs;
using Schematix.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schematix.Core.Mappers;
public interface IBranchMapper
{
    IEnumerable<BranchDto> MapBranches(IEnumerable<Branch> branches);
    IEnumerable<Branch> MapBranchDtos(IEnumerable<BranchDto> branchDtos);

    Branch MapBranchDto(BranchDto dto);
    BranchDto MapBranch(Branch branch);

    
}
public class BranchMapper : IBranchMapper
{
    private readonly IEmployeeMapper _mapper;

    public BranchMapper(IEmployeeMapper mapper)
    {
        _mapper = mapper;
    }
    public BranchDto MapBranch(Branch branch)
    {
        var employees = Enumerable.Empty<EmployeeDto>();

        if (branch.Employees != null) 
        {
            employees = _mapper.MapEmployees(branch.Employees);
        }
         
        return new BranchDto
        {
            Id = branch.Id,
            Name = branch.Name,
            ManagerId = branch.ManagerId,
            Employees = employees
        };
    }

    public Branch MapBranchDto(BranchDto dto)
    {
        var manager = _mapper.MapEmployeeDto(dto.Manager);

        var employees = _mapper.MapEmployeesDto(dto.Employees);
        return new Branch
        {
            Id = dto.Id,
            Name = dto.Name,
            ManagerId = dto.ManagerId,
            Employees = employees as ICollection<Employee>
        };
    }

    public IEnumerable<Branch> MapBranchDtos(IEnumerable<BranchDto> branchDtos)
    {
        return branchDtos.Select(MapBranchDto);
    }

    public IEnumerable<BranchDto> MapBranches(IEnumerable<Branch> branches)
    {
        return branches.Select(MapBranch);
    }
}
