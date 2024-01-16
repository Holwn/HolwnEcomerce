﻿using HolwnEcommerce.Emailing;
using HolwnEcommerce.Public.Orders;
using HolwnEcommerce.Public.Web.Extensions;
using HolwnEcommerce.Public.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Emailing;
using Volo.Abp.TextTemplating;

namespace HolwnEcommerce.Public.Web.Pages.Cart
{
    public class CheckoutModel : PageModel
    {
        private readonly IOrdersAppService _ordersAppService;
        private readonly IEmailSender _emailSender;
        private readonly ITemplateRenderer _templateRenderer;
        public CheckoutModel(IOrdersAppService ordersAppService,
            IEmailSender emailSender,
            ITemplateRenderer templateRenderer)
        {
            _ordersAppService = ordersAppService;
            _emailSender = emailSender;
            _templateRenderer = templateRenderer;
        }
        public List<CartItem> CartItems { get; set; }
        public bool? CreateStatus { get; set; }

        [BindProperty]
        public OrderDto Order { get; set; }
        public void OnGet()
        {
            CartItems = GetCartItems();
        }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid == false)
            {

            }
            var cartItems = new List<OrderItemDto>();
            foreach(var item in GetCartItems())
            {
                cartItems.Add(new OrderItemDto
                {
                    Price = item.Product.SellPrice,
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity
                });
            }
            Guid? currentUserId = User.Identity.IsAuthenticated ? User.GetUserId() : Guid.Empty;
            var order = await _ordersAppService.CreateAsync(new CreateOrderDto()
            {
                CustomerName = Order.CustomerName,
                CustomerAddress = Order.CustomerAddress,
                CustomerPhoneNumber = Order.CustomerPhoneNumber,
                CustomerUserId = currentUserId,
                Items = cartItems
            });
            CartItems = GetCartItems();
            if (order != null)
            {
                //if (User.Identity.IsAuthenticated)
                //{
                //    var email = User.GetSpecificClaim(ClaimTypes.Email);
                //    var emailBody = await _templateRenderer.RenderAsync(
                //        EmailTemplates.CreateOrderEmail,
                //        new
                //        {
                //            message = "Create order success"
                //        });
                //    await _emailSender.SendAsync(email, "Tạo đơn hàng thành công", emailBody);
                //}
                CreateStatus = true;
            }
            else
                CreateStatus = false;
        }

        private List<CartItem> GetCartItems()
        {
            var cart = HttpContext.Session.GetString(HolwnEcommerceConsts.Cart);
            var productCarts = new Dictionary<string, CartItem>();
            if (cart != null)
            {
                productCarts = JsonSerializer.Deserialize<Dictionary<string, CartItem>>(cart);
            }
            return productCarts.Values.ToList();
        }
    }
}
