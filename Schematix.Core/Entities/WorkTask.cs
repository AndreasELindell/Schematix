using Schematix.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Schematix.Core.Entities;

public class WorkTask
{
    [Key]
    public int Id { get; set; }
    [Required]
    public TimeOnly Start { get; set; }
    [Required]
    public TimeOnly End { get; set; }
    [Required]
    public TaskType Type { get; set; }

}