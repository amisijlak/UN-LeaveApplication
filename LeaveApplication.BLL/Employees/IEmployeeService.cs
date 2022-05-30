using LeaveApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.BLL.Employees
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Save the employee details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        (bool, string) SaveEmployee(Employee model);
        /// <summary>
        /// Retrieve the employee records from the database
        /// </summary>
        /// <returns></returns>
        IEnumerable<Employee> GetEmployees();
    }
}
