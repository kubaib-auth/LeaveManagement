using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.LeaveRepositorys.Dtos
{
    public class LeaveQuotaDto : Entity<int>
    {
        public string EmployeeStatus { get; set; }
        public decimal Sick { get; set; }
        public decimal Casual { get; set; }
        public decimal Annual { get; set; }
        public decimal TotalLeave { get; set; }
    }
}
