using AutoMapper;
using HolwnEcommerce.Admin.ProductCategories;
using HolwnEcommerce.Admin.Products;
using HolwnEcommerce.ProductCategories;
using HolwnEcommerce.Products;

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
    }
}
