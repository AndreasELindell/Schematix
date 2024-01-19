using Schematix.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schematix.Core.Interfaces;

public interface IBranchRepository
{
    Task<IEnumerable<Branch>> GetAllBranches();
    Task AddEmployeeToBranch(Employee employee, int branchId);
    Task RemoveEmployeeFromBranch(Employee employee, int branchId);
    Task AddBranch(Branch branch);
    Task DeleteBranch(Branch branch);
    Task UpdateBranch(Branch branch);
    Task<bool> BranchExist(int branchId);
    Task<Branch?> GetBranchByIdWithoutEmployees(int branchId);
    Task<Branch?> GetBranchByIdWithEmployees(int branchId);
}
