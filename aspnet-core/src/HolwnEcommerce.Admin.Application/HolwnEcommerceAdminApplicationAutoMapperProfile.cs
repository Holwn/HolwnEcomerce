using AutoMapper;
using HolwnEcommerce.Admin.ProductCategories;
using HolwnEcommerce.ProductCategories;

namespace HolwnEcommerce.Admin;

public class HolwnEcommerceAdminApplicationAutoMapperProfile : Profile
{
    public HolwnEcommerceAdminApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<ProductCategory, ProductCategoryInListDto>();
        CreateMap<CreateUpdateProductCategoryDto, ProductCategory>();
    }
}
