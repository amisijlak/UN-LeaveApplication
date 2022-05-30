using LeaveApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.BLL.Departments
{
    public interface IDepartmentService
    {
        /// <summary>
        /// Get Departments
        /// </summary>
        /// <returns></returns>
        IQueryable<Department> GetDepartments();
        (bool, string) SaveDepartment(Department model);
    }
}
