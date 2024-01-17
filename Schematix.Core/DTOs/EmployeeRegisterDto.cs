using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schematix.Core.DTOs
{
    public sealed class EmployeeRegisterDto
    {
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public string BranchId { get; set; }
        public required string Phone { get; set; }
        public required string Salary { get; set; }
        public required string Role { get; set; }
    }
}
