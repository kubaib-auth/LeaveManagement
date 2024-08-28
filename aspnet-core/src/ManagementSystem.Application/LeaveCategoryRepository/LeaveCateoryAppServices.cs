using Abp;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Localization;
using ManagementSystem.Authorization.Roles;
using ManagementSystem.Authorization.Users;
using ManagementSystem.LeaveRepositorys.Dtos;
using ManagementSystem.Leaves;
using ManagementSystem.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Abp.UI;

namespace ManagementSystem.LeaveCategoryRepository
{
    public class LeaveCateoryAppServices: ManagementSystemAppServiceBase, ILeaveCateoryAppServices
    {
        private readonly IRepository<Leave> _leaveRepository;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IPermissionManager _permissionManager;
        private readonly ILocalizationContext _localizationContext;

        private readonly IRepository<LeaveCategory> _leaveCategoryRepository;
        public LeaveCateoryAppServices(
            IRepository<Leave> leaveRepository,
            IRepository<LeaveCategory> leaveCategoryRepository,
            RoleManager roleManager,
            UserManager userManager, IPermissionManager permissionManager, ILocalizationContext localizationContext) 
        {
            _leaveRepository = leaveRepository;
            _leaveCategoryRepository = leaveCategoryRepository;
           _roleManager = roleManager;
            _userManager = userManager;
            _permissionManager = permissionManager;
           
            _localizationContext = localizationContext;

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
        //async Task<PagedResultDto<LeaveCategory>> LeaveCategory(GetAllLeaveInput input)
        public async Task<PagedResultDto<LeaveCategoryDto>> LeaveCategory(GetAllLeaveCateoryInput input)
        {
            var categories = await _leaveCategoryRepository.GetAllListAsync();

            var results = categories.Select(o => new LeaveCategoryDto
            {
                Id = o.Id,
                LeaveType = o.LeaveType
            }).ToList();

            return new PagedResultDto<LeaveCategoryDto>
            {
                Items = results,
                TotalCount = results.Count
            };
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
        //public async Task<ListResultDto<PermissionDto>> GetAllPermissionsPC(EntityDto<long> input)
        //{
        //    try
        //    {
        //        var user = await UserManager.GetUserByIdAsync(input.Id);
        //        var userPermissions = await UserManager.GetGrantedPermissionsAsync(user);
        //        var allPermissions = PermissionManager.GetAllPermissions();

        //        var result = allPermissions
        //            .Where(p => p.Parent == null && userPermissions.Contains(p))
        //            .Select(p => new PermissionDto
        //            {
        //                Name = p.Name,
        //                DisplayName = p.DisplayName.Localize(_localizationContext),
        //                ParentName = null, // Root permissions don't have a parent
        //                Children = p.Children
        //                    .Where(c => userPermissions.Contains(c))
        //                    .Select(c => new PermissionDto
        //                    {
        //                        Name = c.Name,
        //                        DisplayName = c.DisplayName.Localize(_localizationContext),
        //                        ParentName = p.Name,
        //                        Children = c.Children
        //                            .Where(cc => userPermissions.Contains(cc))
        //                            .Select(cc => new PermissionDto
        //                            {
        //                                Name = cc.Name,
        //                                DisplayName = cc.DisplayName.Localize(_localizationContext),
        //                                ParentName = c.Name,
        //                                Children = cc.Children
        //                                    .Where(ccc => userPermissions.Contains(ccc))
        //                                    .Select(ccc => new PermissionDto
        //                                    {
        //                                        Name = ccc.Name,
        //                                        DisplayName = ccc.DisplayName.Localize(_localizationContext),
        //                                        ParentName = cc.Name
        //                                    }).ToList()
        //                            }).ToList()
        //                    }).ToList()
        //            }).ToList();

        //        return new ListResultDto<PermissionDto>(result);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        // Log the exception (you can use ABP's logging framework)
        //        Logger.Error("An error occurred while getting permissions", ex);

        //        throw new UserFriendlyException("An internal error occurred during your request!");
        //    }
        //}
        public async Task<ListResultDto<PermissionDto>> GetAllPermissionsPC(EntityDto<long> input)
        {
            try
            {
                var user = await UserManager.GetUserByIdAsync(input.Id);
                var userPermissions = await UserManager.GetGrantedPermissionsAsync(user);
                var allPermissions = PermissionManager.GetAllPermissions();

                var result = allPermissions
                    .Where(p => p.Parent == null )
                    .Select(p => new PermissionDto
                    {
                        Name = p.Name,
                        DisplayName = p.DisplayName.Localize(_localizationContext),
                        ParentName = null, // Root permissions don't have a parent
                        IsGranted = userPermissions.Contains(p),
                        Children = GetChildPermissions(p, userPermissions)
                    }).ToList();

                return new ListResultDto<PermissionDto>(result);
            }
            catch (System.Exception ex)
            {
                Logger.Error("An error occurred while getting permissions", ex);
                throw new UserFriendlyException("An internal error occurred during your request!");
            }
        }

        private List<PermissionDto> GetChildPermissions(Permission parent, IReadOnlyList<Permission> userPermissions)
        {
            return parent.Children
                .Where(c => userPermissions.Contains(c))
                .Select(c => new PermissionDto
                {
                    Name = c.Name,
                    DisplayName = c.DisplayName.Localize(_localizationContext),
                    ParentName = parent.Name,
                    IsGranted = userPermissions.Contains(c),
                    Children = GetChildPermissions(c, userPermissions)
                }).ToList();
        }
    }
    
}
