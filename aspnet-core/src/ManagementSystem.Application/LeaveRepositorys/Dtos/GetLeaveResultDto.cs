using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.LeaveRepositorys.Dtos
{
    public class GetLeaveResultDto
    {
        public PagedResultDto<GetLeaveForViewDto> PagedResult { get; set; }
        public LeaveTypeBalanceDto LeaveBalance { get; set; }
    }
}
