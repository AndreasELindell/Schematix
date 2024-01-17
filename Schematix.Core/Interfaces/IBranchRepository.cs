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
    Task<Branch> GetBranchById(int branchId);
    Task AddBranch(Branch branch);
    Task DeleteBranch(int branchId);
    Task<bool> BranchExist(int branchId);
}
