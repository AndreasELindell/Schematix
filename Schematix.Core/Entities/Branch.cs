﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schematix.Core.Entities;

public class Branch
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [ForeignKey(nameof(ManagerId))]
    public int ManagerId { get; set; }
    public virtual Employee Manager { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
}
