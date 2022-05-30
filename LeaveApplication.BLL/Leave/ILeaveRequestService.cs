using LeaveApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.BLL.Leave
{
    public interface ILeaveRequestService
    {
        /// <summary>
        /// Get leave request as ienumerable
        /// </summary>
        /// <returns></returns>
        IEnumerable<LeaveRequest> GetLeaveRequests();
        /// <summary>
        /// Validate leave request before saving it
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        (bool, string) ValidateLeaveRequest(LeaveRequest model);
        /// <summary>
        /// Save leave request , after successfull validation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        (bool, string) SaveLeaveRequest(LeaveRequest model);
    }
}
