namespace Schematix.Core.DTOs
{
    public class BranchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public virtual EmployeeDto Manager { get; set; }
        public virtual IEnumerable<EmployeeDto> Employees { get; set; }
    }
}
