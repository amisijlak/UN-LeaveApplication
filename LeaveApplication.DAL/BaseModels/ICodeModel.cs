using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApplication.DAL.BaseModels
{
    public interface INumericKeyModel
    {
        long Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
