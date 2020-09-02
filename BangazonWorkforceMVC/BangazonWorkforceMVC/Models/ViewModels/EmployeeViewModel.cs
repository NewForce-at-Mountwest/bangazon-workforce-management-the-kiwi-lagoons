using BangazonAPI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforceMVC.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee employee { get; set; }

        // A dropdown list to get all of the departments
        public List<SelectListItem> departments { get; set; } = new List<SelectListItem>();
    }
