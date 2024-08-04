using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Authorization;
using System.Collections.Generic;
using ManagementSystem.Authorization.Users;

namespace ManagementSystem.Roles.Dto
{
    [AutoMapFrom(typeof(Permission))]
    public class PermissionDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ParentName { get; set; }
        public List<PermissionDto> Children { get; set; }
        public bool IsGranted { get; set; }

    }
}
