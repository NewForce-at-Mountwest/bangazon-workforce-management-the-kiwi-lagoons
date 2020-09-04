using System;

namespace BangazonWorkforceMVC.Models
{
    public class ComputerEmployee
    {
        public int Id { get; set; }

        public int ComputerId { get; set; }

        public Computer computer { get; set; }

        public int EmployeeId { get; set; }

        public Employee employee { get; set; }

        public DateTime AssignDate { get; set; }

        public DateTime UnassignDate { get; set; }

    }
}
