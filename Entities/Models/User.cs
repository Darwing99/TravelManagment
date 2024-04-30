using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CreateBy { get; set; }
        public DateTime ModDate { get; set; }
        public DateTime CrDate { get; set; }
        public DateTime Birthday { get; set; }
        public bool Status { get; set; }
        public int RolId { get; set; }
    }
}
