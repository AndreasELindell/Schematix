using Schematix.Core.Entities;
using Schematix.Core.Enums;

namespace Schematix.Core.DTOs
{
    public class ShiftDto
    {
        public int Id { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public TimeSpan Length { get; set; }
        public DateOnly Date { get; set; }
        public BranchDto Branch { get; set; }
        public EmployeeDto Employee { get; set; }
        public string Type { get; set; }
        public IEnumerable<WorkTaskDto> Tasks { get; set; }
    }
}
