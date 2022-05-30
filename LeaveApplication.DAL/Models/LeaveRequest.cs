using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.DAL.Models
{
    [Table("LeaveRequests", Schema = LEAVESchemas.ENTITIES)]
    public class LeaveRequest : BaseAuditTrailFields
    {
        [ForeignKey("Employee")]
        [Display(Name = "Employee")]
        public long EmployeeId { get; set; }
        [ForeignKey("LeaveType")]
        [Display(Name = "Leave Type")]
        public long LeaveTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
