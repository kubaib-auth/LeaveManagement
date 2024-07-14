using ManagementSystem.Authorization.Users;
using ManagementSystem.Leaves.Enum;
using ManagementSystem.Leaves;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace ManagementSystem.LeaveRepositorys.Dtos
{
    public class LeaveDto:Entity<int>
    {
        public long? UserId { get; set; }
        public User UsersFk { get; set; }
        public int? LeaveCategoryId { get; set; }
        public LeaveCategory LeaveCategoryFk { get; set; }
        public decimal? LeaveBalance { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Days { get; set; }
        public string Description { get; set; }
        public EnumLeave Status { get; set; }
        public ApproveRejectedLeave IsLeave { get; set; }
    }
}
