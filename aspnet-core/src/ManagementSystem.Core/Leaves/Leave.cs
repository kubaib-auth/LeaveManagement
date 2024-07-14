using Abp.Domain.Entities;
using ManagementSystem.Authorization.Users;
using ManagementSystem.Leaves.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Leaves
{
    public class Leave:Entity<int> 
    {
        public long? UserId { get; set; }
        [ForeignKey("UserId")]
        public User UsersFk { get; set; }
        public int? LeaveCategoryId { get; set; }
        [ForeignKey("LeaveCategoryId")]
        public LeaveCategory LeaveCategoryFk { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Days { get; set; }
        public string Description { get; set; }      
        public EnumLeave Status { get; set; }
        public ApproveRejectedLeave IsLeave { get; set; }
        public decimal? LeaveBalance { get; set; }

    }
}
