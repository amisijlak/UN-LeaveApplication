using LeaveApplication.BLL.Data;
using LeaveApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.BLL.Departments
{
    public class DepartmentService : IDepartmentService
    {
        protected readonly IDbRepository _repository;
        public DepartmentService(IDbRepository repository)
        {
            this._repository = repository;
        }

        public IQueryable<Department> GetDepartments()
        {
            return _repository.Set<Department>();
        }

        public (bool, string) SaveDepartment(Department model)
        {
            try
            {
                var dbModel = _repository.Set<Department>().Where(r => r.Id == model.Id).SingleOrDefault();
                if (dbModel == null)
                {
                    _repository.Set<Department>().Add(model);
                }
                else
                {
                    _repository.UpdateDatabaseModel<Department>(dbModel, model);
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
