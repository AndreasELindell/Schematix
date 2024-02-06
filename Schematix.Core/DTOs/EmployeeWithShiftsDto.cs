using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schematix.Core.DTOs
{
    public class EmployeeWithShiftsDto
    {
        public EmployeeDto Employee { get; set; }
        public IEnumerable<ShiftDto> Shifts { get; set; }
    }
}
