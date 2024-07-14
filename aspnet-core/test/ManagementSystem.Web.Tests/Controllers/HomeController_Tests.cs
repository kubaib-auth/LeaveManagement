using System.Threading.Tasks;
using ManagementSystem.Models.TokenAuth;
using ManagementSystem.Web.Controllers;
using Shouldly;
using Xunit;

namespace ManagementSystem.Web.Tests.Controllers
{
    public class HomeController_Tests: ManagementSystemWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}