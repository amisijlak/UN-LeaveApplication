using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.DAL.Models
{
    [Table("Departments", Schema = LEAVESchemas.SETTINGS)]
    public class Department : BaseAuditTrailFields
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
