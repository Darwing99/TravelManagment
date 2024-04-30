using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Trip
    {
        public Guid Code { get; set; }
        public DateTime Date { get; set; }
        public int BranchId { get; set; }
        public int CarrierId { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
    }
}
