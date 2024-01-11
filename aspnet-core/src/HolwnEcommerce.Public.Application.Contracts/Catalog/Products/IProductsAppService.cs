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
        Task<PagedResultDto<ProductInListDto>> GetListFilterAsync(ProductListFilterDto input);
        Task<List<ProductInListDto>> GetListAllAsync();
        Task<string> GetThumbnailImageAsync(string fileName);
        Task<List<ProductAttributeValueDto>> GetListProductAttributeAllAsync(Guid productId);
        Task<PagedResultDto<ProductAttributeValueDto>> GetListProductAttributesAsync(ProductAttributeListFilterDto input);

        Task<List<ProductInListDto>> GetListTopSellerAsync(int numberOfRecords);
    }
}
