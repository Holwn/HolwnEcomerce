using HolwnEcommerce.Public.Catalog.ProductCategories;
using HolwnEcommerce.Public.Catalog.Products;
using HolwnEcommerce.Public.Web.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace HolwnEcommerce.Public.Web.Pages;

public class IndexModel : PublicPageModel
{
    private readonly IDistributedCache<HomeCacheItem> _distributedCache;
    private readonly IProductCategoriesAppService _productCategoriesAppService;
    private readonly IProductsAppService _productsAppService;
    public List<ProductCategoryInListDto> Categories { get; set; }
    public List<ProductInListDto> TopSellerProducts { get; set; }
    public IndexModel(IProductCategoriesAppService productCategoriesAppService,
        IProductsAppService productsAppService,
        IDistributedCache<HomeCacheItem> distributedCache
        ) {
        _productCategoriesAppService = productCategoriesAppService;
        _productsAppService = productsAppService;
        _distributedCache = distributedCache;
    }
    public async Task OnGetAsync()
    {
        var cachrItem = await _distributedCache.GetOrAddAsync(HolwnEcommercePublicConsts.CacheKeys.HomeData, async () =>
        {
            var allCategories = await _productCategoriesAppService.GetListAllAsync();
            var rootCategories = allCategories.Where(x => x.ParentId == null).ToList();
            foreach (var category in rootCategories)
            {
                category.Children = rootCategories.Where(x => x.ParentId == category.Id).ToList();
            }

            var topSellerProducts = await _productsAppService.GetListTopSellerAsync(10);

            return new HomeCacheItem()
            {
                TopSellerProducts = topSellerProducts,
                Categories = rootCategories
            };
        },
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(12)
        });
        
        Categories = cachrItem.Categories;
        TopSellerProducts = cachrItem.TopSellerProducts;
    }
}
