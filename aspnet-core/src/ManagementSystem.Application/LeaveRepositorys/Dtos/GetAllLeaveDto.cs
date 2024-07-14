using Abp.Domain.Entities;
using ManagementSystem.Authorization.Users;
using ManagementSystem.Leaves;
using ManagementSystem.Leaves.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.LeaveRepositorys.Dtos
{
    public class GetAllLeaveDto:Entity<int>
    {
        public string EmployeeName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Days { get; set; }
        public string Description { get; set; }
        public string LeaveType { get; set; }
        public long? UserId { get; set; }
        public EnumLeave Status { get; set; }
        public ApproveRejectedLeave IsLeave { get; set; }
        //    public User UsersFk { get; set; }
        //  public LeaveCategory LeaveCategoryFk { get; set; }
    }
}
