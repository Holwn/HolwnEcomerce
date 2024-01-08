using HolwnEcommerce.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HolwnEcommerce.Admin.Permissions;

public class HolwnEcommerceAdminPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //Catalog
        var catalogGroup = context.AddGroup(HolwnEcommerceAdminPermissions.CatalogGroupName, L("Permission:HolwnEcomAdminCatalog"));

        //Add product
        var productPermission = catalogGroup.AddPermission(HolwnEcommerceAdminPermissions.Product.Default, L("Permission:HolwnEcomAdminCatalog.Product"));
        productPermission.AddChild(HolwnEcommerceAdminPermissions.Product.Create, L("Permission:HolwnEcomAdminCatalog.Product.Create"));
        productPermission.AddChild(HolwnEcommerceAdminPermissions.Product.Update, L("Permission:HolwnEcomAdminCatalog.Product.Update"));
        productPermission.AddChild(HolwnEcommerceAdminPermissions.Product.Delete, L("Permission:HolwnEcomAdminCatalog.Product.Delete"));
        productPermission.AddChild(HolwnEcommerceAdminPermissions.Product.AttributeManage, L("Permission:HolwnEcomAdminCatalog.Product.AttributeManage"));

        //Add attribute
        var attributePermission = catalogGroup.AddPermission(HolwnEcommerceAdminPermissions.Attribute.Default, L("Permission:HolwnEcomAdminCatalog.Attribute"));
        attributePermission.AddChild(HolwnEcommerceAdminPermissions.Attribute.Create, L("Permission:HolwnEcomAdminCatalog.Attribute.Create"));
        attributePermission.AddChild(HolwnEcommerceAdminPermissions.Attribute.Update, L("Permission:HolwnEcomAdminCatalog.Attribute.Update"));
        attributePermission.AddChild(HolwnEcommerceAdminPermissions.Attribute.Delete, L("Permission:HolwnEcomAdminCatalog.Attribute.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HolwnEcommerceResource>(name);
    }
}
