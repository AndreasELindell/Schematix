using System.ComponentModel.DataAnnotations;

namespace Schematix.Core.Entities;

public class Branch
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int ManagerId { get; set; }

}
