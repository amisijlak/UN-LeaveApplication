using LeaveApplication.BLL.Data;
using LeaveApplication.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.BLL.Leave
{
    public class LeaveRequestService : ILeaveRequestService
    {
        protected readonly IDbRepository _repository;
        public LeaveRequestService(IDbRepository repository)
        {
            this._repository = repository;
        }

        public IEnumerable<LeaveRequest> GetLeaveRequests()
        {
            return _repository.Set<LeaveRequest>().Include(r => r.Employee).Include(r => r.Employee.Department);
        }


        public (bool, string) ValidateLeaveRequest(LeaveRequest model)
        {
            string ErrorMessage = "";
            try
            {
                var LeaveList = GetLeaveRequests().Where(r => r.Id != model.Id);
                var DepartmentId = _repository.Set<Employee>().Where(r => r.Id == model.EmployeeId).SingleOrDefault().DepartmentId;

                if (model.StartDate >= model.EndDate)
                {
                    throw new Exception($"The StartDate {model.StartDate} CAN NOT be greater than the end date {model.EndDate}!");
                }

                var MyLeaveRequests = LeaveList.Where(r => r.EmployeeId == model.EmployeeId);

                if (MyLeaveRequests.Where(r => r.EmployeeId == model.EmployeeId).Any(r => r.EndDate >= model.StartDate))
                {
                    throw new Exception("The dates you have selected ovalap with one of your previous requests!");
                }

                if (LeaveList.Any(r => r.Employee.DepartmentId == DepartmentId && r.EndDate >= model.StartDate))
                {
                    throw new Exception("The dates you have selected ovalap with onether employee in your department!");
                }

                var myLastRequestEndDate = MyLeaveRequests.Max(r => r.EndDate);

                //Here we are just considering 30 days being a months , we did not want to calculate the month based on number of working days
                //, since the requirement did not specify that 
                if (myLastRequestEndDate.HasValue)
                {
                    if (GetNumberOfDays(model.StartDate.Value, myLastRequestEndDate.Value) < 30)
                    {
                        throw new Exception($"You can ONLY qualify for another leave after 30 days from your previous leave, Which ended {myLastRequestEndDate}!");
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message.ToString();
            }

            return (string.IsNullOrEmpty(ErrorMessage), ErrorMessage);
        }

        public double GetNumberOfDays(DateTime date1, DateTime date2)
        {
            return (date1 - date2).TotalDays;
        }

        public (bool, string) SaveLeaveRequest(LeaveRequest model)
        {
            try
            {
                var validationResult = ValidateLeaveRequest(model);
                if (validationResult.Item1)
                {
                    var dbModel = _repository.Set<LeaveRequest>().Where(r => r.Id == model.Id).SingleOrDefault();
                    if (dbModel == null)
                    {
                        _repository.Set<LeaveRequest>().Add(model);
                    }
                    else
                    {
                        _repository.UpdateDatabaseModel<LeaveRequest>(dbModel, model);
                    }
                    _repository.SaveChanges();
                    return (true, "");
                }
                return validationResult;
            }
            catch (Exception e)
            {
                return (false, e.InnerException.ToString());
            }
        }
    }
}
