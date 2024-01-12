using HolwnEcommerce.Public.Catalog.Products;

namespace HolwnEcommerce.Public.Web.Models
{
    public class CartItem
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
