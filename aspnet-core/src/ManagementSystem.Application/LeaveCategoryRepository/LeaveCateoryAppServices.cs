using Abp;
using Abp.Domain.Repositories;
using Abp.Notifications;
using Abp.Runtime.Session;
using ManagementSystem.LeaveRepositorys.Dtos;
using ManagementSystem.Leaves;
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

        private readonly IRepository<LeaveCategory> _leaveCategoryRepository;
        public LeaveCateoryAppServices(
            IRepository<Leave> leaveRepository,
            IRepository<LeaveCategory> leaveCategoryRepository)
        {
            _leaveRepository = leaveRepository;
            _leaveCategoryRepository = leaveCategoryRepository;
           
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
    }
}
