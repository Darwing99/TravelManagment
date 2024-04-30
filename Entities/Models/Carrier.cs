using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Carrier
    {
        public int CarrierId { get; set; }
        public string Name { get; set; }
        public decimal RatePerKilometer { get; set; }
        public bool Status { get; set; }
    }

}
