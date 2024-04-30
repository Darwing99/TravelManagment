using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class EmployeeBranchAssignmen
    {
        public int AssignmentId { get; set; }
        public int EmployeeId { get; set; }
        public int BranchId { get; set; }
        public decimal DistanceToHome { get; set; }
        public bool Status { get; set; }
    }

}
