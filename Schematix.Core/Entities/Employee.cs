using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schematix.Core.Entities
{
    public class Employee : IdentityUser
    {
        public int Salary {  get; set; }
        public int BranchId { get; set; }

    }
}
