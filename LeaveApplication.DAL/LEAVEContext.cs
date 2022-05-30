using LeaveApplication.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.DAL
{
    public class LEAVEContext : DbContext
    {
        public string ConnectionString => _connectionString;
        private readonly string _connectionString;

        public LEAVEContext(DbContextOptions<LEAVEContext> options)
            : base(options)
        {
            if (options != null)
            {
                //extract connnection string
                var extension = options.FindExtension<SqlServerOptionsExtension>();
                _connectionString = extension.ConnectionString;
            }
        }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
