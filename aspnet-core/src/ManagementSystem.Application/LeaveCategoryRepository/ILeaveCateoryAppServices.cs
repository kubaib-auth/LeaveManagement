using Abp.Application.Services.Dto;
using ManagementSystem.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.LeaveCategoryRepository
{
    public interface  ILeaveCateoryAppServices
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissionsPC(EntityDto<long> input);
    }
}
