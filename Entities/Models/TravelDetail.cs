using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{

    public class TravelDetail
    {
        public string NameEmployee { get; set; }
        public DateTime Date { get; set; }
        public decimal DistanceTraveled { get; set; }
        public string CreatedBy { get; set; }
        public int CountEmployees { get; set; }
        public string Name { get; set; }
        public decimal RatePerKilometer { get; set; }
        public decimal Total { get; set; }
    }
}
