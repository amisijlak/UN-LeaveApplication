using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.DAL.Models
{
    [Table("Employees", Schema = LEAVESchemas.SETTINGS)]
    public class Employee : BaseAuditTrailFields
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("Department")]
        public long DepartmentId { get; set; } 

        public virtual Department Department { get; set; }
    }
}
