using Abp.Authorization;
using ManagementSystem.Authorization.Roles;
using ManagementSystem.Authorization.Users;

namespace ManagementSystem.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
