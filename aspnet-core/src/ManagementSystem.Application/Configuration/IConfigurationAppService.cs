using System.Threading.Tasks;
using ManagementSystem.Configuration.Dto;

namespace ManagementSystem.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
