using HolwnEcommerce.Admin.Permissions;
using HolwnEcommerce.ProductAttributes;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HolwnEcommerce.Admin.Catalog.ProductAttributes
{
    [Authorize(HolwnEcommerceAdminPermissions.Attribute.Default, Policy = "AdminOnly")]
    public class ProductAttributesAppService : CrudAppService
        <ProductAttribute,
        ProductAttributeDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateProductAttributeDto,
        CreateUpdateProductAttributeDto>, IProductAttributesAppService
    {
        public ProductAttributesAppService(IRepository<ProductAttribute, Guid> repository)
            : base(repository)
        {
            GetPolicyName = HolwnEcommerceAdminPermissions.Attribute.Default;
            GetListPolicyName = HolwnEcommerceAdminPermissions.Attribute.Default;
            CreatePolicyName = HolwnEcommerceAdminPermissions.Attribute.Create;
            UpdatePolicyName = HolwnEcommerceAdminPermissions.Attribute.Update;
            DeletePolicyName = HolwnEcommerceAdminPermissions.Attribute.Delete;
        }

        [Authorize(HolwnEcommerceAdminPermissions.Attribute.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(HolwnEcommerceAdminPermissions.Attribute.Default)]
        public async Task<List<ProductAttributeInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<ProductAttribute>, List<ProductAttributeInListDto>>(data);
        }

        [Authorize(HolwnEcommerceAdminPermissions.Attribute.Default)]
        public async Task<PagedResultDto<ProductAttributeInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Label.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ProductAttributeInListDto>(totalCount, ObjectMapper.Map<List<ProductAttribute>, List<ProductAttributeInListDto>>(data));
        }
    }
}