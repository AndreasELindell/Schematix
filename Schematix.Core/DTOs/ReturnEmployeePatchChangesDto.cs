using Schematix.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schematix.Core.DTOs
{
    public class ReturnEmployeePatchChangesDto
    {
        public string Property { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }

        public static List<ReturnEmployeePatchChangesDto> GetChanges(Employee original, Employee modified)
        {
            var changes = new List<ReturnEmployeePatchChangesDto>();

            if (original.UserName != modified.UserName)
            {
                changes.Add(new ReturnEmployeePatchChangesDto { Property = "UserName", OldValue = original.UserName, NewValue = modified.UserName });
            }

            if (original.Email != modified.Email)
            {
                changes.Add(new ReturnEmployeePatchChangesDto { Property = "Email", OldValue = original.Email, NewValue = modified.Email });
            }
            if (original.PhoneNumber != modified.PhoneNumber)
            {
                changes.Add(new ReturnEmployeePatchChangesDto { Property = "PhoneNumber", OldValue = original.PhoneNumber, NewValue = modified.PhoneNumber });
            }

            if (original.Salary != modified.Salary)
            {
                changes.Add(new ReturnEmployeePatchChangesDto { Property = "Salary", OldValue = original.Salary, NewValue = modified.Salary });
            }

            return changes;
        }
    }
}
