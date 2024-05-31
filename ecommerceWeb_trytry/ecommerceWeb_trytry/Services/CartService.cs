using ecommerceWeb_trytry.Data;
using ecommerceWeb_trytry.Models;
using Newtonsoft.Json;

namespace ecommerceWeb_trytry.Services
{
    public class CartService : ICartService
    {

        private readonly ProductContext _context;
        private readonly IHttpContextAccessor? _httpContextAccessor; //interface provide httpcontext , for quest etc
        private Cart _cart = new Cart();



        public  CartService(ProductContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _cart = GetCartFromSession();
        }

        public Cart GetCart()
        {
            return _cart;
        }


        public void AddItemToCart(Product product, int quantity)
        {
            var item = _cart.Items.FirstOrDefault(i => i.product.ProductId == product.ProductId);


            if (item == null)
            {
                item = new Item { product = product, Quantity = quantity };
                _cart.Items.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
            UpdateCartInSession();
        }

        private void UpdateCartInSession()
        {

            _httpContextAccessor.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(_cart));
        }



        public void AddToCart(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);

            if (product != null)
            {
                var newItem = new Item { product = product, Quantity = quantity };
                _cart.Items.Add(newItem);
            }

        }


        public void UpdateCart(Cart cart)
        {
            _cart = cart;
            SaveCartToSession(_cart);
        }

        private void SaveCartToSession(Cart carts)
        {
            var cartJson = JsonConvert.SerializeObject(carts);
            _httpContextAccessor.HttpContext.Session.SetString("Cart", cartJson);
        }


        private Cart GetCartFromSession()
        {

            var cartJson = _httpContextAccessor.HttpContext.Session.GetString("Cart");

            if (!string.IsNullOrEmpty(cartJson))
            {
                return JsonConvert.DeserializeObject<Cart>(cartJson);
            }

            var cart = new Cart();
            _httpContextAccessor.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            return cart;
        }



        public void RemoveItemFromCart(int productId)
        {
            var cart = GetCart();
            var itemToRemove = cart.Items.FirstOrDefault(i => i.product.ProductId == productId);
            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
                UpdateCartInSession();
            }


        }

        public void ClearCart()
        {
            _httpContextAccessor.HttpContext.Session.Remove("Cart");
        }


    }
}
