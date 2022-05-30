using LeaveApplication.BLL.Leave;
using LeaveApplication.DAL.Models;
using LeaveApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveApplication.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestService leaveService;
        public LeaveRequestController(ILeaveRequestService leaveService)
        {
            this.leaveService = leaveService;
        }

        public IActionResult Index(SettingsListModel model)
        {
            model.LoadELeaveRequests(leaveService);
            return View(model);
        }

        public IActionResult Details(long Id = 0)
        {
            return View(leaveService.GetLeaveRequests().Where(r => r.Id == Id).SingleOrDefault() ?? new LeaveRequest());
        }

        [HttpPost]
        public IActionResult Details(LeaveRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = leaveService.SaveLeaveRequest(model);
                if (result.Item1)
                {
                    return RedirectToAction("Index", new { SearchTerm = model.EmployeeId });
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