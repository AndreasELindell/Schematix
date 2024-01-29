using Schematix.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schematix.Core.Entities
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public TimeOnly Start { get; set; }
        [Required]
        public TimeOnly End { get; set; }
        [Required]
        public TimeSpan Length { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [ForeignKey(nameof(BranchId))]
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public ShiftType Type { get; set; }
        public virtual ICollection<WorkTask> Tasks { get; set; }


    }
}
