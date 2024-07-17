using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.LeaveRepositorys.Dtos
{
    public class LeaveTypeBalanceDto
    {
        public decimal CasualLeaveBalance {  get; set; }
        public decimal AnnualLeaveBalance { get; set; }

        public decimal SickLeaveBalance { get; set; }
        public decimal CasualLeaveAsign { get; set; }
        public decimal AnnualLeaveAsign { get; set; }
        public decimal SickLeaveAsign { get; set; }
        public decimal TotalAvailableLeaveBalance { get; set; }
        public decimal TotalAvailedLeaveBalance { get; set; }
        

    }
}
