﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Schematix.Core.Entities;

namespace Schematix.Infrastructure.Context
{
    public class DataContext : IdentityDbContext<Employee>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { }


        DbSet<Employee> Employees { get; set;}
        DbSet<Shift> Shifts { get; set;}
        DbSet<Branch> Branches { get; set;}
    }
}
