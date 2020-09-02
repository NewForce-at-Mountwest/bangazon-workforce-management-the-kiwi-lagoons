using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Display(Name = "Department")]

        public string Name { get; set; }

        public int Budget { get; set; }

        public List<Employee> listOfEmployees { get; set; } = new List<Employee>();
    }
}
