using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Leaves
{
    public class LeaveCategory : Entity<int>
    {
        public string LeaveType { get; set; }
        
    }
}
