using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ManagementSystem.Controllers
{
    public abstract class ManagementSystemControllerBase: AbpController
    {
        protected ManagementSystemControllerBase()
        {
            LocalizationSourceName = ManagementSystemConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
