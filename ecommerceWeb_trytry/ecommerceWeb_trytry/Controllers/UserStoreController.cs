using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerceWeb_trytry.Data;
using ecommerceWeb_trytry.Models;
using ecommerceWeb_trytry_trail2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class UserStoreController : Controller
    {
        private readonly ProductContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStoreService _userService;

        public UserStoreController(UserManager<IdentityUser> userManager, IUserStoreService userService, ProductContext context, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _userService = userService;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SellerProducts(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var user = await _userManager.GetUserAsync(User);
                email = user.Email;
            }

            var products = await _userService.GetProductsBySellerEmailAsync(email);
            return View(products);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SortProducts(string email, string sortOrder)
        {
            if (string.IsNullOrEmpty(email))
            {
                var user = await _userManager.GetUserAsync(User);
                email = user.Email;
            }

            var products = await _userService.GetProductsBySellerEmailAsync(email);

            // Sort products
            products = sortOrder switch
            {
                "date" => products.OrderBy(p => p.ProductProduceTime),
                "date_desc" => products.OrderByDescending(p => p.ProductProduceTime),
                "type" => products.OrderBy(p => p.ProductType),
                "type_desc" => products.OrderByDescending(p => p.ProductType),
                _ => products // Default case: no sorting
            };

            return View("SellerProducts", products);
        }
    }
}