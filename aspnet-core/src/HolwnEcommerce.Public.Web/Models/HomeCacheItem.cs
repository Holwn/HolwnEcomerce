using HolwnEcommerce.Public.Catalog.ProductCategories;
using HolwnEcommerce.Public.Catalog.Products;
using System.Collections.Generic;

namespace HolwnEcommerce.Public.Web.Models
{
    public class HomeCacheItem
    {
        public List<ProductCategoryInListDto> Categories { get; set; }
        public List<ProductInListDto> TopSellerProducts { get; set; }
    }
}
