using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Obligatorisk_Opgave_4._Semester.Models;

namespace Obligatorisk_Opgave_4._Semester.Services
{
    public interface ICartService
    {
        Cart GetCart();
        bool AddToCart(int productId, int quantity);
        void ClearCart();
    }

    public class SessionCartService : ICartService
    {
        private const string SessionKey = "Cart";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Obligatorisk_Opgave_4._Semester.Abstractions.IProductService _productService;

        private ISession? Session => _httpContextAccessor.HttpContext?.Session;

        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public SessionCartService(IHttpContextAccessor httpContextAccessor, Obligatorisk_Opgave_4._Semester.Abstractions.IProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
        }

        public Cart GetCart()
        {
            if (Session == null) return new Cart();
            var data = Session.GetString(SessionKey);
            if (string.IsNullOrEmpty(data)) return new Cart();
            return JsonSerializer.Deserialize<Cart>(data, _serializerOptions) ?? new Cart();
        }

        public bool AddToCart(int productId, int quantity)
        {
            if (Session == null) return false;
            var product = _productService.GetById(productId);
            if (product == null) return false;
            if (quantity <= 0) return false;
            if (!_productService.TryReduceStock(productId, quantity)) return false;

            var cart = GetCart();
            var existing = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity
                });
            }

            Session.SetString(SessionKey, JsonSerializer.Serialize(cart, _serializerOptions));
            return true;
        }

        public void ClearCart()
        {
            if (Session == null) return;
            var cart = GetCart();
            // Return stock
            foreach (var item in cart.Items)
            {
                _productService.IncreaseStock(item.ProductId, item.Quantity);
            }

            Session.Remove(SessionKey);
        }
    }
}
