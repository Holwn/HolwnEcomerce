using HolwnEcommerce.Public.Catalog.ProductCategories;
using HolwnEcommerce.Public.Catalog.Products;
using System.Collections.Generic;

namespace HolwnEcommerce.Public.Web.Models
{
    public class HeaderCacheItem
    {
        public List<ProductCategoryInListDto> Categories { get; set; }
    }
}
