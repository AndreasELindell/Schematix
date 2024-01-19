using Microsoft.AspNetCore.Identity;

namespace Schematix.Core.Entities
{
    public class Employee : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salary { get; set; }
        public virtual ICollection<Shift> Shifts { get; }
    }
}
