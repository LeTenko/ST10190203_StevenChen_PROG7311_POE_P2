
using ecommerceWeb_trytry.Models;





namespace ecommerceWeb_trytry.Services
{
    public interface ICartService
    {

        void AddItemToCart(Product product, int quantity);
        void RemoveItemFromCart(int productId);
        void ClearCart();
        Cart GetCart();




    }
}
