using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Schematix.Core.Entities;

namespace Schematix.Infrastructure.Context
{
    public class DataContext : IdentityDbContext<Employee>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { }


        public DbSet<Employee> Employees { get; set;}
        public DbSet<Shift> Shifts { get; set;}
        public DbSet<Branch> Branches { get; set;}
    }
}
