using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.LeaveRepositorys.Dtos
{
    public class UserDetailDto
    {
        public decimal CasualLeaveBalance { get; set; }
        public decimal AnnualLeaveBalance { get; set; }

        public decimal SickLeaveBalance { get; set; }
        public decimal CasualLeaveAsign { get; set; }
        public decimal AnnualLeaveAsign { get; set; }
        public decimal SickLeaveAsign { get; set; }
        public decimal TotalAvailableLeaveBalance { get; set; }
        public decimal TotalAvailedLeaveBalance { get; set; }
        public string EmployeeName { get; set; }
        public string FatherName { get; set; }
        public string ReportingTo { get; set; }
        public string Email { get; set; }
        public string CNIC { get; set; }
        public string MobileNo { get; set; }
        public string ReferenceMobileNo { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string PermanentAddress { get; set; }
        public string EmployeeStatus { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal PercentageCasualUsed { get; set; }
        public decimal PercentageAnnualUsed { get; set; }
        public decimal PercentageSickUsed { get; set; }

    }
}
