using HolwnEcommerce.Orders;
using HolwnEcommerce.Products;
using HolwnEcommerce.Public.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HolwnEcommerce.Public.Orders
{
    public class OrdersAppService : CrudAppService<
        Order,
        OrderDto,
        Guid,
        PagedResultRequestDto, CreateOrderDto>, IOrdersAppService
    {
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly OrderCodeGenerator _orderCodeGenerator;
        private readonly IRepository<Product,Guid> _productRepository;
        public OrdersAppService(IRepository<Order, Guid> orderRepository,
            IRepository<OrderItem> orderItemRepository,
            OrderCodeGenerator orderCodeGenerator,
            IRepository<Product, Guid> productRepository)
            : base(orderRepository)
        {
            _orderItemRepository = orderItemRepository;
            _orderCodeGenerator = orderCodeGenerator;
            _productRepository = productRepository;
        }

        public override async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            var subTotal = input.Items.Sum(x => x.Quantity * x.Price);
            var orderId = Guid.NewGuid();
            var order = new Order(orderId)
            {
                Code = await _orderCodeGenerator.GenerateAsync(),
                CustomerAddress = input.CustomerAddress,
                CustomerName = input.CustomerName,
                CustomerPhoneNumber = input.CustomerPhoneNumber,
                ShippingFee = 0,
                CustomerUserId = input.CustomerUserId,
                Tax = 0,
                Subtotal = subTotal,
                GrandTotal = subTotal,
                Discount = 0,
                PaymentMethod = PaymentMethod.COD,
                Total = subTotal,
                Status = OrderStatus.New
            };
            var items = new List<OrderItem>();
            foreach (var item in input.Items)
            {
                var product = await _productRepository.GetAsync(item.ProductId);
                items.Add(new OrderItem
                {
                    OrderId = orderId,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    SKU = product.SKU,
                });
            }
            await _orderItemRepository.InsertManyAsync(items);
            var result = await Repository.InsertAsync(order);
            
            return ObjectMapper.Map<Order, OrderDto>(result);
        }
    }
}
