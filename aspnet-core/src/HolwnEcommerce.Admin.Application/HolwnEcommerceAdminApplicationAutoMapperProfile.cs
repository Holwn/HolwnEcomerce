using AutoMapper;
using HolwnEcommerce.Admin.Catalog.Manufacturers;
using HolwnEcommerce.Admin.Catalog.ProductAttributes;
using HolwnEcommerce.Admin.Catalog.ProductCategories;
using HolwnEcommerce.Admin.Catalog.Products;
using HolwnEcommerce.Admin.System.Roles;
using HolwnEcommerce.Admin.System.Users;
using HolwnEcommerce.Manufacturers;
using HolwnEcommerce.ProductAttributes;
using HolwnEcommerce.ProductCategories;
using HolwnEcommerce.Products;
using HolwnEcommerce.Roles;
using Volo.Abp.Identity;

namespace HolwnEcommerce.Admin;

public class HolwnEcommerceAdminApplicationAutoMapperProfile : Profile
{
    public HolwnEcommerceAdminApplicationAutoMapperProfile()
    {
        //Product Category
        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<ProductCategory, ProductCategoryInListDto>();
        CreateMap<CreateUpdateProductCategoryDto, ProductCategory>();

        //Product
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductInListDto>();
        CreateMap<CreateUpdateProductDto, Product>();

        //Manufacturer
        CreateMap<Manufacturer, ManufacturerDto>();
        CreateMap<Manufacturer, ManufacturerInListDto>();
        CreateMap<CreateUpdateManufacturerDto, Manufacturer>();

        //Product Attribute
        CreateMap<ProductAttribute, ProductAttributeDto>();
        CreateMap<ProductAttribute, ProductAttributeInListDto>();
        CreateMap<CreateUpdateProductAttributeDto, ProductAttribute>();

        //Role
        CreateMap<IdentityRole, RoleDto>().ForMember(x => x.Description,
            map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName)
            ? x.ExtraProperties[RoleConsts.DescriptionFieldName]
            : null));
        CreateMap<IdentityRole, RoleInListDto>()
            .ForMember(x => x.Description,
            map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName)
            ? x.ExtraProperties[RoleConsts.DescriptionFieldName]
            : null));
        CreateMap<CreateUpdateRoleDto, IdentityRole>();

        //User
        CreateMap<IdentityUser, UserDto>();
        CreateMap<IdentityUser, UserInListDto>();
    }
}
