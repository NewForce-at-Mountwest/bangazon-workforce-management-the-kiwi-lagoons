using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangazonWorkforceMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]

        public string FirstName { get; set; }

        [Display(Name = "Last Name")]

        public string LastName { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public Department department { get; set; }

        [Display(Name = "Supervisor")]
        public bool isSupervisor { get; set; }

        public Computer computer { get; set; }

        public List<TrainingProgram> TrainingPrograms { get; set; } = new List<TrainingProgram>();


    }
}
