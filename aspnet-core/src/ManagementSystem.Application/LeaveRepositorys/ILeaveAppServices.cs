using Abp.Application.Services.Dto;
using ManagementSystem.LeaveRepositorys.Dtos;
using ManagementSystem.Leaves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.LeaveRepositorys
{
    public interface ILeaveAppServices
    {
        Task<PagedResultDto<GetLeaveForViewDto>> GetAll(GetAllLeaveInput input);
       // Task<GetLeaveResultDto> GetAll(GetAllLeaveInput input);
        Task CreateOrEdit(LeaveDto input);
        Task<Leave> GetId(int id);
        Task<List<GetAllLeaveDto>> GetDetailId(int id);
        Task Delete(int id);
        Task ApprovedLeave(int input);
        Task<EmployeeDto> GetCurrentUser();
        Task<List<LeaveCategoryDto>> GetAllLeaveCategory();
        Task<List<EmployeeDto>> GetAllUsers();
        Task RejectedLeave(int input);
    }
}
