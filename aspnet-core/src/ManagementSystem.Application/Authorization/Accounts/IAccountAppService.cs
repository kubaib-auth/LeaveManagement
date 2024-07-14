using System.Threading.Tasks;
using Abp.Application.Services;
using ManagementSystem.Authorization.Accounts.Dto;

namespace ManagementSystem.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
