using Schematix.Core.Entities;

namespace Schematix.Core.DTOs
{
    public class ShiftDto
    {
        public int Id { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public TimeSpan Length { get; set; }
        public DateOnly Date { get; set; }
        public Branch Branch { get; set; }
        public Employee Employee { get; set; }
    }
}
