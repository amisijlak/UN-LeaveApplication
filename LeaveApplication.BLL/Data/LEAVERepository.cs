using LeaveApplication.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.BLL.Data
{
    public class LEAVERepository : IDbRepository
    {
        private readonly LEAVEContext _database;


        public LEAVERepository(LEAVEContext db)
        {
            this._database = db;
        }

        public DbSet<T> Set<T>() where T : class
        {
            return _database.Set<T>();
        }

        public void UpdateDatabaseModel<T>(T dbItem, T updatedItem) where T : class
        {
            _database.Entry(dbItem).CurrentValues.SetValues(updatedItem);
        }

        public void SaveChanges()
        {
            _database.SaveChanges();
        }

        public string GetConnectionString()
        {
            return _database?.ConnectionString;
        }

        public void Remove<T>(T model) where T : class
        {
            _database.Remove(model);
        }
    }
}