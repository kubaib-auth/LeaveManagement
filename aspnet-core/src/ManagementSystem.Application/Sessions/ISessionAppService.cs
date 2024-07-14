using System.Threading.Tasks;
using Abp.Application.Services;
using ManagementSystem.Sessions.Dto;

namespace ManagementSystem.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
