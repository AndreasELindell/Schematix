using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Schematix.Core.Entities;
using Schematix.Core.Enums;

namespace Schematix.Infrastructure.Context
{
    public class DataContext : IdentityDbContext<Employee>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkTask>()
                .Property(e => e.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (TaskType)Enum.Parse(typeof(TaskType), v));

            modelBuilder.Entity<Shift>()
                .Property(e => e.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (ShiftType)Enum.Parse(typeof(ShiftType), v));
        }

        public DbSet<Employee> Employees { get; set;}
        public DbSet<Shift> Shifts { get; set;}
        public DbSet<Branch> Branches { get; set;}
        public DbSet<WorkTask> Tasks { get; set;}
    }
}
