using HolwnEcommerce.Admin.Permissions;
using HolwnEcommerce.ProductCategories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace HolwnEcommerce.Admin.Catalog.ProductCategories
{
    [Authorize(HolwnEcommerceAdminPermissions.ProductCategory.Default, Policy = "AdminOnly")]
    public class ProductCategoriesAppService : CrudAppService
        <ProductCategory,
        ProductCategoryDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateProductCategoryDto,
        CreateUpdateProductCategoryDto>, IProductCategoriesAppService
    {
        public ProductCategoriesAppService(IRepository<ProductCategory, Guid> repository)
            : base(repository)
        {
            GetPolicyName = HolwnEcommerceAdminPermissions.ProductCategory.Default;
            GetListPolicyName = HolwnEcommerceAdminPermissions.ProductCategory.Default;
            CreatePolicyName = HolwnEcommerceAdminPermissions.ProductCategory.Create;
            UpdatePolicyName = HolwnEcommerceAdminPermissions.ProductCategory.Update;
            DeletePolicyName = HolwnEcommerceAdminPermissions.ProductCategory.Delete;
        }

        [Authorize(HolwnEcommerceAdminPermissions.ProductCategory.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(HolwnEcommerceAdminPermissions.ProductCategory.Default)]
        public async Task<List<ProductCategoryInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryInListDto>>(data);
        }

        [Authorize(HolwnEcommerceAdminPermissions.ProductCategory.Default)]
        public async Task<PagedResultDto<ProductCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ProductCategoryInListDto>(totalCount, ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryInListDto>>(data));
        }
    }
}
