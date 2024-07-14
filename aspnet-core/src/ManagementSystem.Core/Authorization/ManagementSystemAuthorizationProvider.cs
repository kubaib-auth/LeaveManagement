using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace ManagementSystem.Authorization
{
    public class ManagementSystemAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
          
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Leaves, L("Leaves"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_AllLeaves, L("AllLeaves"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_OwnLeave, L("OwnLeave"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_LeaveApprover, L("LeaveApprover"), multiTenancySides: MultiTenancySides.Host);

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ManagementSystemConsts.LocalizationSourceName);
        }
    }
}
