using HolwnEcommerce.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HolwnEcommerce.Public.Permissions;

public class HolwnEcommercePublicPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HolwnEcommercePublicPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(PublicPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HolwnEcommerceResource>(name);
    }
}