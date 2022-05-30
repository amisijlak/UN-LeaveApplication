using LeaveApplication.BLL.Data;
using LeaveApplication.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.BLL.Employees
{
    public class EmployeeService : IEmployeeService
    {
        protected readonly IDbRepository _repository;

        public EmployeeService(IDbRepository repository)
        {
            this._repository = repository;
        }


        public IEnumerable<Employee> GetEmployees()
        {
            return _repository.Set<Employee>().Include(r => r.Department);
        }

        public (bool, string) SaveEmployee(Employee model)
        {
            try
            {
                var dbModel = _repository.Set<Employee>().Where(r => r.Id == model.Id).SingleOrDefault();
                if (dbModel == null) //New Employee
                {
                    _repository.Set<Employee>().Add(model);
                }
                else
                {
                    _repository.UpdateDatabaseModel<Employee>(dbModel, model);
                }
                _repository.SaveChanges();
                return (true, "");
            }
            catch (Exception e)
            {
                return (false, e.InnerException.ToString());
            }
        }
    }
}