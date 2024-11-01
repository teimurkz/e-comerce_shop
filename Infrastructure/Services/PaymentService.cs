using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entites;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _config;
        private readonly ICartService _cartService;
        private readonly IUnitOfWork _unit;

        public PaymentService(IConfiguration config, ICartService cartService, IUnitOfWork unit)
        {
            _config = config;
            _cartService = cartService;
            _unit = unit;
        }

        public async Task<ShoppingCart?> CreateOrUpdatePaymentsIntent(string cartId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
            var cart = await _cartService.GetCartAsync(cartId);
            if (cart == null) return null;

            var shippingPrice = 0m;

            if (cart.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unit.Repository<DeliveryMethod>().GetByIdAsync((int)cart.DeliveryMethodId);
                if (deliveryMethod == null) return null;

                shippingPrice = deliveryMethod.Price;
            }

            foreach (var item in cart.Items)
            {
                var productItem = await _unit.Repository<Core.Entites.Product>().GetByIdAsync(item.ProductId);
                if (productItem == null) return null;
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }

            var service = new PaymentIntentService();
            PaymentIntent? intent = null;
            if (string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" } // Исправлено
                };
                intent = await service.CreateAsync(options);
                cart.PaymentIntentId = intent.Id;
                cart.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice * 100
                };
                intent = await service.UpdateAsync(cart.PaymentIntentId, options);
            }

            await _cartService.SetCartAsync(cart);
            return cart;
        }


    }
}
