using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ManagementSystem.Configuration.Dto;

namespace ManagementSystem.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ManagementSystemAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
