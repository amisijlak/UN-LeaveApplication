using LeaveApplication.BLL.Departments;
using LeaveApplication.DAL.Models;
using LeaveApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveApplication.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        public IActionResult Index(SettingsListModel model)
        {
            model.LoadData(departmentService);
            return View(model);
        }

        public IActionResult Details(long Id = 0)
        {
            return View(departmentService.GetDepartments().Where(r=>r.Id == Id).SingleOrDefault() ?? new Department());
        }

        [HttpPost]
        public IActionResult _Save(Department model)
        {
            if (ModelState.IsValid)
            {
                var result = departmentService.SaveDepartment(model);
                if (result.Item1)
                {
                    return RedirectToAction("Index", new { SearchTerm = model.Name });
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
