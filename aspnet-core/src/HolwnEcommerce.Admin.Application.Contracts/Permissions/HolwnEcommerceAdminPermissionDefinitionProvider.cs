using HolwnEcommerce.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HolwnEcommerce.Admin.Permissions;

public class HolwnEcommerceAdminPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HolwnEcommerceAdminPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(HolwnEcommercePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HolwnEcommerceResource>(name);
    }
}
