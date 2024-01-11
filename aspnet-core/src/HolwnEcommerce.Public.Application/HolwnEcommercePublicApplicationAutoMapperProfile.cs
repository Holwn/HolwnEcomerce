using AutoMapper;
using HolwnEcommerce.Manufacturers;
using HolwnEcommerce.ProductAttributes;
using HolwnEcommerce.ProductCategories;
using HolwnEcommerce.Products;
using HolwnEcommerce.Public.Catalog.Manufacturers;
using HolwnEcommerce.Public.Catalog.ProductAttributes;
using HolwnEcommerce.Public.Catalog.ProductCategories;
using HolwnEcommerce.Public.Catalog.Products;

namespace HolwnEcommerce.Public;

public class HolwnEcommercePublicApplicationAutoMapperProfile : Profile
{
    public HolwnEcommercePublicApplicationAutoMapperProfile()
    {
        //Product Category
        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<ProductCategory, ProductCategoryInListDto>();

        //Product
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductInListDto>();

        //Manufacturer
        CreateMap<Manufacturer, ManufacturerDto>();
        CreateMap<Manufacturer, ManufacturerInListDto>();

        //Product Attribute
        CreateMap<ProductAttribute, ProductAttributeDto>();
        CreateMap<ProductAttribute, ProductAttributeInListDto>();
    }
}
