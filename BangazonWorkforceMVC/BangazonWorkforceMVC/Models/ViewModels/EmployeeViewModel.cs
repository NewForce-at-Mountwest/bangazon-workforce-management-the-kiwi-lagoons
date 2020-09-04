using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BangazonWorkforceMVC.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee employee { get; set; }

        // A dropdown list to get all of the departments
        public List<SelectListItem> departments { get; set; } = new List<SelectListItem>();

        public TrainingProgram TrainingProgram { get; set; }
    }
}
