using System;
using System.Collections.Generic;
using System.Text;

namespace HolwnEcommerce.Public.Catalog.Products
{
    public class ProductListFilterDto : BaseListFilterDto
    {
        public Guid? CategoryId { get; set; }
    }
}
