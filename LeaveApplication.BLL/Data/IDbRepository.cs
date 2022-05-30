using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.BLL.Data
{
    /// <summary>
    /// IDBrepository for implementing Repository pattern
    /// </summary>
    public interface IDbRepository
    {
        /// <summary>
        /// Set the model using generics
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        DbSet<T> Set<T>() where T : class;

        void UpdateDatabaseModel<T>(T dbItem, T updatedItem) where T : class;

        void SaveChanges();

        string GetConnectionString();
        void Remove<T>(T model) where T : class;
    }
}
        