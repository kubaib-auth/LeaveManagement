using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using ManagementSystem.Authorization.Users;
using ManagementSystem.LeaveRepositorys.Dtos;
using ManagementSystem.Leaves;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Runtime.Security;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using ManagementSystem.Leaves.Enum;
using Castle.MicroKernel.Internal;
using Abp.Extensions;
using Microsoft.AspNetCore.Http;
using Abp.Authorization;
using ManagementSystem.Authorization;
using Abp.Notifications;
using Abp;

namespace ManagementSystem.LeaveRepositorys
{
    public class LeaveAppServices : ManagementSystemAppServiceBase, ILeaveAppServices
    {
        private readonly IRepository<Leave> _leaveRepository;
        private readonly IRepository<LeaveCategory> _leaveCategoryRepository;
        private readonly IRepository<LeaveQuota> _leaveQuotaRepository;
        private readonly UserManager _userManager;
        private readonly IAbpSession _abpSession;
        private readonly INotificationPublisher _notificationPublisher;
        public LeaveAppServices(
            IRepository<Leave> leaveRepository,
            IRepository<LeaveCategory> leaveCategoryRepository,
            UserManager userManager, IAbpSession abpSession, IRepository<LeaveQuota> leaveQuotaRepository, INotificationPublisher notificationPublisher)
        {
            _leaveRepository = leaveRepository;
            _leaveCategoryRepository = leaveCategoryRepository;
            _userManager = userManager;
            _abpSession = abpSession;
            _leaveQuotaRepository = leaveQuotaRepository;
            _notificationPublisher = notificationPublisher;
        }
        public async Task<PagedResultDto<GetLeaveForViewDto>> GetAll(GetAllLeaveInput input)
        {
            int currentUserId = (int)AbpSession.UserId;
            if (currentUserId == null)
            {
                throw new AbpAuthorizationException("User is not logged in.");
            }

            var hasPermission = await PermissionChecker.IsGrantedAsync(PermissionNames.Pages_AllLeaves);

            var filteredLeaves = _leaveRepository.GetAll()
        .Include(e => e.LeaveCategoryFk)
        .Include(e => e.UsersFk)
        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.UsersFk.UserName.Trim().ToLower().Contains(input.Filter.Trim().ToLower()) || e.LeaveCategoryFk.LeaveType.Trim().ToLower().Contains(input.Filter.Trim().ToLower()))
        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.UsersFk.UserName.Trim().ToLower() == input.NameFilter.Trim().ToLower() || e.LeaveCategoryFk.LeaveType.Trim().ToLower() == input.NameFilter.Trim().ToLower())
        .WhereIf(!string.IsNullOrWhiteSpace(input.CategoryNameFilter), e => e.LeaveCategoryFk != null && e.LeaveCategoryFk.LeaveType.Trim().ToLower() == input.CategoryNameFilter.Trim().ToLower())
        .WhereIf(input.LeaveFiterId.HasValue, e => e.UsersFk != null && e.UsersFk.Id == input.LeaveFiterId)
        .WhereIf(!hasPermission, e => e.UserId == currentUserId);


            //var pagedAndFilteredLeaves = filteredLeaves.PageBy(input);
            var pagedAndFilteredLeaves = filteredLeaves.OrderBy(input.Sorting ?? "id asc")
                .PageBy(input.SkipCount, input.MaxResultCount);
            var leaveDetails = from leave in pagedAndFilteredLeaves
                               join user in _userManager.Users on leave.UserId equals user.Id
                               join leaveCategory in _leaveCategoryRepository.GetAll() on leave.LeaveCategoryId equals leaveCategory.Id into leaveCategoryJoin
                               from lc in leaveCategoryJoin.DefaultIfEmpty()
                               select new
                               {
                                   leave.Id,
                                   leave.Description,
                                   leave.FromDate,
                                   leave.ToDate,
                                   leave.Days,
                                   leave.Status,
                                   leave.UserId,
                                   leave.IsLeave,
                                   leave.LeaveBalance,
                                   EmployeeName = user.UserName,
                                   LeaveType = lc == null ? "" : lc.LeaveType,
                                   LeaveCategoryId = lc != null ? (int?)lc.Id : null
                               };

            var totalCount = await filteredLeaves.CountAsync();
            var dbList = await leaveDetails.ToListAsync();
            var singleUserId = input.LeaveFiterId ?? currentUserId; 
            var userDbList = dbList.Where(o => o.UserId == singleUserId).ToList();
            var userLeaveBalance = await GetBalance((int)singleUserId);

            var results = userDbList.Select(o => new GetLeaveForViewDto
            {
                Product = new LeaveDto
                {
                    Id = o.Id,

                    Description = o.Description,
                    FromDate = o.FromDate,
                    ToDate = o.ToDate,
                    Days = o.Days,
                    Status = o.Status,
                    UserId = o.UserId,
                    IsLeave = o.IsLeave,
                    LeaveBalance = o.LeaveBalance,
                },
                EmployeeName = o.EmployeeName,
                LeaveType = o.LeaveType,
                CasualLeaveBalance = userLeaveBalance.CasualLeaveBalance,
                AnnualLeaveBalance = userLeaveBalance.AnnualLeaveBalance,
                SickLeaveBalance= userLeaveBalance.SickLeaveBalance,
                CasualLeaveAsign= userLeaveBalance.CasualLeaveAsign,

                AnnualLeaveAsign= userLeaveBalance.AnnualLeaveAsign,
                SickLeaveAsign = userLeaveBalance.SickLeaveAsign
                ,

            }).ToList();

            return new PagedResultDto<GetLeaveForViewDto>(
                totalCount,
                results
            );
        }


        public async Task CreateOrEdit(LeaveDto input)
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
        private async Task Create(LeaveDto input)
        {
            //TimeSpan ts = (input.ToDate - input.FromDate).Duration();
            //int days = ts.Days;

            var leaveda = new Leave()
            {
                UserId = input.UserId,
                LeaveCategoryId = input.LeaveCategoryId,
                FromDate = input.FromDate,
                ToDate = input.ToDate,
                Description = input.Description,
                Status = input.Status,
                Days = input.Days,
                LeaveBalance = 0,
                //  Days= days.ToString(),
            };
            var message = "Khubaib";
            await _leaveRepository.InsertAsync(leaveda);
            var defaultTenantAdmin = new UserIdentifier(1, 2);
            await _notificationPublisher.PublishAsync(
               "App.SimpleMessage",
               new MessageNotificationData(message),
               severity: NotificationSeverity.Info,
               userIds: new[] { defaultTenantAdmin }
           );
        }
        private async Task Update(LeaveDto input)
        {
            var leaveToUpdate = await _leaveRepository.FirstOrDefaultAsync(l => l.Id == input.Id);
            if (leaveToUpdate == null)
            {
                throw new ApplicationException("Leave not found.");
            }
            leaveToUpdate.UserId = input.UserId;
            leaveToUpdate.LeaveCategoryId = input.LeaveCategoryId;
            leaveToUpdate.FromDate = input.FromDate;
            leaveToUpdate.ToDate = input.ToDate;
            leaveToUpdate.Days = input.Days;
            leaveToUpdate.Description = input.Description;
            leaveToUpdate.Status = input.Status;
            leaveToUpdate.LeaveBalance = 0;
            await _leaveRepository.UpdateAsync(leaveToUpdate);
        }
        public async Task<Leave> GetId(int id)
        {
            var leave = await _leaveRepository.FirstOrDefaultAsync(x => x.Id == id);
            return leave;
        }

        public async Task<List<GetAllLeaveDto>> GetDetailId(int id)
        {

            var leaveDetailId = await _leaveRepository.GetAll().Where(a => a.Id == id)
                .Select(x => new GetAllLeaveDto
                {
                    Id = x.Id,
                    EmployeeName = x.UsersFk.UserName,
                    LeaveType = x.LeaveCategoryFk.LeaveType,
                    Days = x.Days,
                    Description = x.Description,
                    Status = x.Status,
                    IsLeave = x.IsLeave,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                }).ToListAsync();
            return leaveDetailId;
        }
        public async Task ApprovedLeave(int input)
        {
            try
            {
                var approveLeave = await _leaveRepository.FirstOrDefaultAsync(x => x.Id == input);

                approveLeave.IsLeave = ApproveRejectedLeave.Approved;
                decimal leaveIncrement = (decimal)approveLeave.LeaveBalance;
                if (!int.TryParse(approveLeave.Days, out int days))
                {
                    throw new Exception("Invalid number of days.");
                }
                for (int i = 1; i <= days; i++)
                {
                    switch (approveLeave.Status)
                    {
                        case EnumLeave.Half:
                            approveLeave.LeaveBalance = leaveIncrement += 0.50m;
                            break;
                        case EnumLeave.Short:
                            approveLeave.LeaveBalance = leaveIncrement += 0.25m;
                            break;
                        case EnumLeave.Full:
                            approveLeave.LeaveBalance = leaveIncrement += 1.00m;
                            break;
                    }

                }


                await _leaveRepository.UpdateAsync(approveLeave);
                CurrentUnitOfWork.SaveChanges();
            }
            catch
            {

            }

        }
        public async Task RejectedLeave(int input)
        {
            var rejectedLeave = await _leaveRepository.FirstOrDefaultAsync(x => x.Id == input);

            rejectedLeave.IsLeave = ApproveRejectedLeave.Rejected;
            await _leaveRepository.UpdateAsync(rejectedLeave);
            CurrentUnitOfWork.SaveChanges();
        }
        public async Task Delete(int id)
        {
            await _leaveRepository.DeleteAsync(id);
        }

        public async Task<List<EmployeeDto>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.Select(user => new EmployeeDto
            {
                Id = user.Id,
                EmployeeName = user.UserName
            }).ToList();
        }
        public async Task<List<LeaveCategoryDto>> GetAllLeaveCategory()
        {
            var leaveCategory = await _leaveCategoryRepository.GetAllListAsync();
            return leaveCategory.Select(leaveCategory => new LeaveCategoryDto
            {
                Id = leaveCategory.Id,
                LeaveType = leaveCategory.LeaveType
            }).ToList();
        }
        public async Task<EmployeeDto> GetCurrentUser()
        {
            var userId = _abpSession.UserId;
            var user = await _userManager.Users.Where(x => x.Id == userId).Select(x => new EmployeeDto 
            { 
                Id = x.Id, 
                EmployeeName = x.UserName 
            }).FirstOrDefaultAsync();
            return user;
        }

        private async Task<LeaveTypeBalanceDto> GetBalance(int id)
        {

            //  var hasPermission = await PermissionChecker.IsGrantedAsync(PermissionNames.Pages_AllLeaves);
            var allLeaves = await _leaveRepository.GetAllListAsync(x => x.UserId == id);
            var userLeave = await _userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

            //var allLeaves = await _leaveRepository.GetAllListAsync(x => x.UserId == AbpSession.UserId);
            //var userLeave = await _userManager.Users.Where(x => x.Id == AbpSession.UserId).FirstOrDefaultAsync();

            var assignedLeaveBalances = await _leaveQuotaRepository.GetAllListAsync(v => v.Id == userLeave.LeaveQuotaId);
            int year = DateTime.Now.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            DateTime lastDay = new DateTime(year, 12, 31);
            int totalDaysInYear = (lastDay - firstDay).Days + 1;
            DateTime joinUser = userLeave.JoiningDate;
            int remaingDays = (lastDay - joinUser).Days;
            var getLeave = new LeaveTypeBalanceDto();

            foreach (var leaveQuota in assignedLeaveBalances)
            {
                if (leaveQuota.EmployeeStatus!=null)
                {
                    var casualLeaves = allLeaves.Where(leave => leave.LeaveCategoryId == 1);
                    var annualLeaves = allLeaves.Where(leave => leave.LeaveCategoryId == 2);
                    var sickLeaves = allLeaves.Where(leave => leave.LeaveCategoryId == 3);


                    decimal totalCasualLeaveBalance = casualLeaves.Sum(leave => leave.LeaveBalance ?? 0);
                    decimal totalAnnualLeaveBalance = annualLeaves.Sum(leave => leave.LeaveBalance ?? 0);
                    decimal totalSickLeaveBalance = sickLeaves.Sum(leave => leave.LeaveBalance ?? 0);
                    decimal totalAvailedleaveBalance = totalCasualLeaveBalance + totalSickLeaveBalance + totalAnnualLeaveBalance;

                    decimal casualLeaveAsignUser = (leaveQuota.Casual / totalDaysInYear) * remaingDays;
                    decimal AnnualLeaveAsignUser = (leaveQuota.Annual / totalDaysInYear) * remaingDays;
                    decimal SickLeaveAsignUser = (leaveQuota.Sick / totalDaysInYear) * remaingDays;
                    decimal totalAvailableBalance = casualLeaveAsignUser + AnnualLeaveAsignUser + SickLeaveAsignUser;

                    //decimal remainingCasualLeave = casualLeaveAsignUser - totalCasualLeaveBalance;
                    //decimal remainingAnnualLeave = AnnualLeaveAsignUser - totalAnnualLeaveBalance;
                    //decimal remainingSickLeave = SickLeaveAsignUser - totalSickLeaveBalance;

                    getLeave.CasualLeaveAsign = casualLeaveAsignUser;
                    getLeave.AnnualLeaveAsign = AnnualLeaveAsignUser;
                    getLeave.SickLeaveAsign = SickLeaveAsignUser;
                    getLeave.CasualLeaveBalance = totalCasualLeaveBalance;
                    getLeave.AnnualLeaveBalance = totalAnnualLeaveBalance;
                    getLeave.SickLeaveBalance = totalSickLeaveBalance;
                    getLeave.TotalAvailableLeaveBalance = totalAvailableBalance;
                    getLeave.TotalAvailedLeaveBalance = totalAvailedleaveBalance;
                    break;
                }
            }
            return getLeave;
        }


    }
}

