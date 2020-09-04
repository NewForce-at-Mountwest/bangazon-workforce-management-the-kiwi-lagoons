using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Models
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

        public TrainingProgram trainingProgram { get; set; }

        public List<TrainingProgram> listOfTrainingPrograms { get; set; } = new List<TrainingProgram>();

    }
}
