namespace Entities.Models
{
    public class EmployeeByBranch
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal DistanceToHome { get; set; } 
        public bool Chequed { get; set; }   
    }
}
