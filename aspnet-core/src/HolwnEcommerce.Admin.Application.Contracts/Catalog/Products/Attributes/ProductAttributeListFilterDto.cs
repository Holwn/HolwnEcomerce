using System;
using System.Collections.Generic;
using System.Text;

namespace HolwnEcommerce.Admin.Catalog.Products.Attributes
{
    public class ProductAttributeListFilterDto : BaseListFilterDto
    {
        public Guid ProductId { get; set; }
    }
}
