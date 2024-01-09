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

        //Add Manufacturer 
        var manufacturerPermission = catalogGroup.AddPermission(HolwnEcommerceAdminPermissions.Manufacturer.Default, L("Permission:HolwnEcomAdminCatalog.Manufacturer"));
        manufacturerPermission.AddChild(HolwnEcommerceAdminPermissions.Manufacturer.Create, L("Permission:HolwnEcomAdminCatalog.Manufacturer.Create"));
        manufacturerPermission.AddChild(HolwnEcommerceAdminPermissions.Manufacturer.Update, L("Permission:HolwnEcomAdminCatalog.Manufacturer.Update"));
        manufacturerPermission.AddChild(HolwnEcommerceAdminPermissions.Manufacturer.Delete, L("Permission:HolwnEcomAdminCatalog.Manufacturer.Delete"));

        //Add product category
        var ProductCategoryPermission = catalogGroup.AddPermission(HolwnEcommerceAdminPermissions.ProductCategory.Default, L("Permission:HolwnEcomAdminCatalog.ProductCategory"));
        ProductCategoryPermission.AddChild(HolwnEcommerceAdminPermissions.ProductCategory.Create, L("Permission:HolwnEcomAdminCatalog.ProductCategory.Create"));
        ProductCategoryPermission.AddChild(HolwnEcommerceAdminPermissions.ProductCategory.Update, L("Permission:HolwnEcomAdminCatalog.ProductCategory.Update"));
        ProductCategoryPermission.AddChild(HolwnEcommerceAdminPermissions.ProductCategory.Delete, L("Permission:HolwnEcomAdminCatalog.ProductCategory.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HolwnEcommerceResource>(name);
    }
}
