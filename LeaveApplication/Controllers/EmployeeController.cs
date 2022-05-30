using LeaveApplication.BLL.Employees;
using LeaveApplication.DAL.Models;
using LeaveApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public IActionResult Index(SettingsListModel model)
        {
            model.LoadEmployees(employeeService);
            return View(model);
        }

        public IActionResult Details(long Id = 0)
        {
            return View(employeeService.GetEmployees().Where(r => r.Id == Id).SingleOrDefault() ?? new Employee());
        }

        [HttpPost]
        public IActionResult Details(Employee model)
        {
            if (ModelState.IsValid)
            {
                var result = employeeService.SaveEmployee(model);
                if (result.Item1)
                {
                    return RedirectToAction("Index", new { SearchTerm = model.FirstName });
                }
            }
            else
            {
                model.ErrorMessage = "Please fill all required fields!";
            }

            return View(model);
        }
    }
}
