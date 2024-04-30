using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TripDetail
    {
        public int TripDetailId { get; set; }
        public Guid TripCode { get; set; }
        public int EmployeeId { get; set; }
        public decimal DistanceTraveled { get; set; }
        public int CountEmployees { get; set; } 
        public bool Status { get; set; }
    }

}
