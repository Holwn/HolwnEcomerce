using System;
using System.Collections.Generic;
using System.Text;

namespace HolwnEcommerce.Public.Catalog.Products.Attributes
{
    public class ProductAttributeListFilterDto : BaseListFilterDto
    {
        public Guid ProductId { get; set; }
    }
}
