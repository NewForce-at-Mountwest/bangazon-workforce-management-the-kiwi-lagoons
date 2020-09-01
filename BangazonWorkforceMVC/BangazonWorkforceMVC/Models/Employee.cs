﻿namespace BangazonAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int DepartmentId { get; set; }

        public Department department { get; set; }

        public bool isSupervisor { get; set; }

        public Computer computer { get; set; }

    }
}
