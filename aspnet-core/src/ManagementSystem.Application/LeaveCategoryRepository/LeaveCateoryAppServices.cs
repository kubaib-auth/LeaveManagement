using Abp;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Notifications;
using Abp.Runtime.Session;
using ManagementSystem.Authorization.Roles;
using ManagementSystem.Authorization.Users;
using ManagementSystem.LeaveRepositorys.Dtos;
using ManagementSystem.Leaves;
using ManagementSystem.Roles.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.LeaveCategoryRepository
{
    public class LeaveCateoryAppServices: ManagementSystemAppServiceBase, ILeaveCateoryAppServices
    {
        private readonly IRepository<Leave> _leaveRepository;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;

        private readonly IRepository<LeaveCategory> _leaveCategoryRepository;
        public LeaveCateoryAppServices(
            IRepository<Leave> leaveRepository,
            IRepository<LeaveCategory> leaveCategoryRepository,
            RoleManager roleManager,
            UserManager userManager) 
        {
            _leaveRepository = leaveRepository;
            _leaveCategoryRepository = leaveCategoryRepository;
           _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task CreateOrEdit(LeaveCategoryDto input)
        {
            if (input.Id == 0 || input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }
        private async Task Create(LeaveCategoryDto input)
        {
           
            var leaveCateory = new LeaveCategory()
            {
              
                LeaveType = input.LeaveType,
               
            };
            await _leaveCategoryRepository.InsertAsync(leaveCateory);
            
        }
        private async Task Update(LeaveCategoryDto input)
        {
            var leaveCateoryToUpdate = await _leaveCategoryRepository.FirstOrDefaultAsync(l => l.Id == input.Id);
            if (leaveCateoryToUpdate == null)
            {
                throw new ApplicationException("Leave not found.");
            }
            leaveCateoryToUpdate.Id = (int)input.Id;
            leaveCateoryToUpdate.LeaveType = input.LeaveType;

            await _leaveCategoryRepository.UpdateAsync(leaveCateoryToUpdate);
        }
        public async Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
            var roleEditDto = ObjectMapper.Map<RoleEditDto>(role);

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }
    }
}
