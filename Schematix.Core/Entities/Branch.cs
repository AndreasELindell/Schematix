using System.ComponentModel.DataAnnotations;

namespace Schematix.Core.Entities;

public class Branch
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public int ManagerId { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
}
