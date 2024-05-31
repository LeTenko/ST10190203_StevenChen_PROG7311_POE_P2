using ecommerceWeb_trytry.Models;
using ecommerceWeb_trytry.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ecommerceWeb_trytry.Data;




namespace ecommerceWeb_trytry.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ProductContext _context;

        public CartController(CartService cartService, ProductContext context)
        {
            _cartService= cartService;
            _context = context;
        }


        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            return View(cart);

        }


        [HttpPost]
        public IActionResult AddToCart([FromBody] AddToCartViewModel model)
        {


            if(ModelState.IsValid)
            {
                var cart= _cartService.GetCart();

                var existingItem = cart.Items.FirstOrDefault(item => item.product.ProductId== model.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity = model.Quantity;
                    _cartService.UpdateCart(cart);

                    return Json(new { Updated = true });
                }

                _cartService.AddToCart(model.ProductId, model.Quantity);
                _cartService.UpdateCart(cart);

                return Json(new { Updated = false });

            }

            return BadRequest();


        }


        [HttpPost]
        public IActionResult Remove(int productid)
        {
            _cartService.RemoveItemFromCart(productid);
            return RedirectToAction("Index","Cart");
        }


    }

}
