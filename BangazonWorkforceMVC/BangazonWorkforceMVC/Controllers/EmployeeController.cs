﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Models;
using BangazonWorkforceMVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace BangazonWorkforceMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _config;

        public EmployeeController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
            SELECT e.Id,
                e.FirstName,
                e.LastName,
                d.Name
            FROM Employee e
            JOIN Department d
            ON e.DepartmentId = d.Id
        ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Employee> employees = new List<Employee>();
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            department = new Department
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            }

                        };

                        employees.Add(employee);
                    }

                    reader.Close();

                    return View(employees);
                }
            }
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select d.Id, d.Name FROM Department d";

                    SqlDataReader reader = cmd.ExecuteReader();

                    EmployeeViewModel viewModel = new EmployeeViewModel();

                    while (reader.Read())
                    {
                        Department department = new Department()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        SelectListItem departmentOptionTag = new SelectListItem()
                        {
                            Text = department.Name,
                            Value = department.Id.ToString()
                        };

                        viewModel.departments.Add(departmentOptionTag);
                    }

                    reader.Close();

                    return View(viewModel);
                }
            }
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel viewModel)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO Employee
                ( FirstName, LastName, DepartmentId, IsSupervisor )
                VALUES
                ( @firstName, @lastName, @DepartmentId, @IsSupervisor )";
                        cmd.Parameters.Add(new SqlParameter("@firstName", viewModel.employee.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@lastName", viewModel.employee.LastName));
                        cmd.Parameters.Add(new SqlParameter("@departmentId", viewModel.employee.DepartmentId));
                        cmd.Parameters.Add(new SqlParameter("@isSupervisor", viewModel.employee.isSupervisor));
                        cmd.ExecuteNonQuery();

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT e.Id, e.FirstName, e.LastName, e.DepartmentId, e.IsSupervisor, ce.ComputerId, ce.EmployeeId, c.Make FROM Employee e
Left JOIN ComputerEmployee ce ON e.Id = ce.EmployeeId
LEFT JOIN Computer c ON c.Id = ce.ComputerId
WHERE e.Id  = @id
AND ce.UnassignDate IS Null ";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Create a new instance of our view model
                    EmployeeViewModel viewModel = new EmployeeViewModel();

                    Employee employee = null;

                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            isSupervisor = reader.GetBoolean(reader.GetOrdinal("IsSupervisor")),
                            computer = new Computer
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ComputerId")),
                                Make = reader.GetString(reader.GetOrdinal("Make"))
                            }
                        };
                    }
                    reader.Close();
                    viewModel.employee = employee;

                    

                    // Select all the departments
                    cmd.CommandText = @"SELECT Department.Id, Department.Name FROM Department";

                    reader = cmd.ExecuteReader();

                    // Create a new instance of our view model

                    while (reader.Read())
                    {
                        // Map the raw data to our department model
                        Department department = new Department
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        // Use the info to build our SelectListItem
                        SelectListItem departmentOptionTag = new SelectListItem()
                        {
                            Text = department.Name,
                            Value = department.Id.ToString()
                        };

                        // Add the select list item to our list of dropdown options
                        viewModel.departments.Add(departmentOptionTag);

                    }

                    reader.Close();

                    // Select all the computers currently assigned to the user or available to be assigned
                    cmd.CommandText = @"SELECT c.Id, c.DecomissionDate, c.Make, c.Manufacturer, ce.Id as 'ComputerEmployee Id', ce.ComputerId, ce.EmployeeId, ce.AssignDate, ce.UnassignDate
FROM Computer c 
LEFT JOIN ComputerEmployee ce
ON c.Id = ce.ComputerId
WHERE (AssignDate IS NULL AND DecomissionDate IS NULL)
OR (ComputerId != ALL (Select ComputerId FROM ComputerEmployee WHERE UnassignDate IS NULL) AND DecomissionDate IS NULL)
OR (EmployeeId = @Id AND UnassignDate is NULL)  ";

                    reader = cmd.ExecuteReader();
                    List<int> idTracker = new List<int>();

                    viewModel.computers.Add(new SelectListItem { Value = "0", Text = "Choose a computer" });
                    while (reader.Read())
                    {
                        

                        if (!idTracker.Any(listId => listId == reader.GetInt32(reader.GetOrdinal("Id"))))
                        {
                            // Map the raw data to our computer model
                            Computer computer = new Computer
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Make = reader.GetString(reader.GetOrdinal("Make"))
                            };

                            // Use the info to build our SelectListItem
                            SelectListItem computerOptionTag = new SelectListItem()
                            {
                                Text = computer.Make,
                                Value = computer.Id.ToString()
                            };


                            // Add the select list item to our list of dropdown options
                                viewModel.computers.Add(computerOptionTag);
                                    
                            

                        }

                        idTracker.Add(reader.GetInt32(reader.GetOrdinal("Id")));

                        Console.WriteLine();
                       

                    }

                    return View(viewModel);
                }


            }
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeViewModel viewModel)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE Employee
                                            SET FirstName = @FirstName,
                                                LastName = @LastName,
                                                IsSupervisor = @isSupervisor,
                                                DepartmentId = @DepartmentId
                                                WHERE Id = @id;
                                            UPDATE ComputerEmployee
                                            SET UnassignDate = @unassignDate
                                            WHERE EmployeeId=@employeeId AND UnassignDate IS NULL; 
                                            INSERT INTO ComputerEmployee
                                            ( EmployeeId, ComputerId, AssignDate )
                                            VALUES
                                            ( @employeeId, @computerId, @assignDate );"; 
                        cmd.Parameters.Add(new SqlParameter("@FirstName", viewModel.employee.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@LastName", viewModel.employee.LastName));
                        cmd.Parameters.Add(new SqlParameter("@isSupervisor", viewModel.employee.isSupervisor));
                        cmd.Parameters.Add(new SqlParameter("@DepartmentId", viewModel.employee.DepartmentId));
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.Parameters.Add(new SqlParameter("@employeeId", id));
                        cmd.Parameters.Add(new SqlParameter("@unassignDate", DateTime.Now));
                        cmd.Parameters.Add(new SqlParameter("@computerId", viewModel.employee.computer.Id));
                        cmd.Parameters.Add(new SqlParameter("@assignDate", DateTime.Now));


                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        throw new Exception("No rows affected");
                    }
                }
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
