using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.DAL.BaseModels
{
    public interface IAuditrail
    {
        string CreatedBy { get; set; }
        string CreatedDate { get; set; }
        string ModifiedBy { get; set; }
        string ModifiedDate { get; set; }
    }
}
