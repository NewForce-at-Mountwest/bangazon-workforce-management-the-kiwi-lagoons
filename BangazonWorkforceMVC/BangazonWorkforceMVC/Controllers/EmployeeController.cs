using BangazonWorkforceMVC.Models;
using BangazonWorkforceMVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

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
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    // Select a single employee using SQL by their id
                    cmd.CommandText = @"
                    SELECT e.Id,
                    e.FirstName,
                    e.LastName,
                    e.isSuperVisor,
                    d.Name,
                    p.Manufacturer,
                    p.Make,
                    r.Name AS 'Training Name'

                    FROM Employee e

                    LEFT JOIN Department d
                    ON e.DepartmentId = d.Id

                    LEFT JOIN ComputerEmployee c
                    ON e.Id = c.EmployeeId

                    LEFT JOIN Computer p
                    ON c.ComputerId = p.Id

                    LEFT JOIN EmployeeTraining t
                    ON e.Id = t.EmployeeId

                    LEFT JOIN TrainingProgram r
                    ON t.TrainingProgramId = r.Id

                    WHERE e.id = @id

                    AND c.UnassignDate is null
                ";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Map the raw SQL data to an employee model
                    Employee employee = null;

                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            isSupervisor = reader.GetBoolean(reader.GetOrdinal("isSuperVisor")),
                            department = new Department
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            },
                            computer = new Computer
                            {
                                Manufacturer = reader.GetString(reader.GetOrdinal("Manufacturer")),
                                Make = reader.GetString(reader.GetOrdinal("Make"))
                            }
                            //listOfTrainingPrograms = reader.GetString(reader.GetOrdinal("listOfTrainingPrograms"))
                        };

                        while (reader.Read())
                        {
                            TrainingProgram newTrainingPrograms = new TrainingProgram
                            {
                                Name = reader.GetString(reader.GetOrdinal("Training Name"))
                            };

                            employee.TrainingPrograms.Add(newTrainingPrograms);
                        }

                    }

                    reader.Close();

                    // If we got something back to the db, send us to the details view
                    if (employee != null)
                    {
                        return View(employee);
                    }
                    else
                    {
                        // If we didn't get anything back from the db, we made a custom not found page down here
                        return RedirectToAction(nameof(NotFound));
                    }

                }
            }
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
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
