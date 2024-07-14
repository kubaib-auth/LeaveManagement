using Abp.Application.Services;
using ManagementSystem.MultiTenancy.Dto;

namespace ManagementSystem.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

