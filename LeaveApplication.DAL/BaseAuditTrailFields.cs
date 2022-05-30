using LeaveApplication.DAL.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.DAL
{
    public class BaseAuditTrailFields : IAuditrail
    {
        [Key]
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }

        [NotMapped]
        public string ErrorMessage { get; set; }
    }
}
