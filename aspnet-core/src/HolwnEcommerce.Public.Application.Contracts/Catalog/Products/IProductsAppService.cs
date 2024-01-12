using HolwnEcommerce.Public.Catalog.ProductCategories;
using HolwnEcommerce.Public.Catalog.Products.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HolwnEcommerce.Public.Catalog.Products
{
    public interface IProductsAppService : IReadOnlyAppService
        <ProductDto,
        Guid, PagedResultRequestDto>
    {
        Task<PagedResult<ProductInListDto>> GetListFilterAsync(ProductListFilterDto input);
        Task<List<ProductInListDto>> GetListAllAsync();
        Task<string> GetThumbnailImageAsync(string fileName);
        Task<List<ProductAttributeValueDto>> GetListProductAttributeAllAsync(Guid productId);
        Task<PagedResult<ProductAttributeValueDto>> GetListProductAttributesAsync(ProductAttributeListFilterDto input);

        Task<List<ProductInListDto>> GetListTopSellerAsync(int numberOfRecords);
        Task<ProductDto> GetBySlugAsync(string slug);
    }
}
