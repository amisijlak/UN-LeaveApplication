using LeaveApplication.BLL.Departments;
using LeaveApplication.BLL.Employees;
using LeaveApplication.BLL.Leave;
using LeaveApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveApplication.Models
{
    public class SettingsListModel
    {
        public string SearchTerm { get; set; }
        public string ErrorMessage { get; set; }
        public List<Department> Items { get; set; }
        public List<Employee> Employees { get; set; }
        public List<LeaveRequest> LeaveRequests { get; set; }

        public void LoadData(IDepartmentService departmentService)
        {
            try
            {
                var query = departmentService.GetDepartments();

                foreach (var term in GetSearchTerms(SearchTerm))
                {
                    query = query.Where(r => r.Name.Contains(term));
                }

                Items = query.ToList();
            }
            catch (Exception e)
            {
                ErrorMessage = e.InnerException.ToString();
            }
        }

        public string[] GetSearchTerms(string Search)
        {
            return SearchTerm.Split(" ");
        }
        public void LoadEmployees(IEmployeeService employeeService)
        {
            try
            {
                var query = employeeService.GetEmployees();

                foreach (var term in GetSearchTerms(SearchTerm))
                {
                    query = query.Where(r => r.FirstName.Contains(term) || r.LastName.Contains(term));
                }

                Employees = query.ToList();
            }
            catch (Exception e)
            {
                ErrorMessage = e.InnerException.ToString();
            }
        }

        public void LoadELeaveRequests(ILeaveRequestService leaveService)
        {
            try
            {
                var query = leaveService.GetLeaveRequests();

                LeaveRequests = query.ToList();
            }
            catch (Exception e)
            {
                ErrorMessage = e.InnerException.ToString();
            }
        }
    }
}
