using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ecommerceWeb_trytry.Data;
using ecommerceWeb_trytry.Models;
using ecommerceWeb_trytry.Services;

namespace ecommerceWeb_trytry.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductContext _context;
        private readonly CartService _cartService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ProductContext context, CartService cartService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _cartService = cartService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
       
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            var cart = _cartService.GetCart();

            foreach (var item in cart.Items)
            {
                var product = products.FirstOrDefault(p => p.ProductId == item.product.ProductId);
                if (product != null)
                {
                    product.Quantity = item.Quantity;
                }
            }

            return View(products);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }


        [Authorize]
        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName,ProductPrice,ProductImage,ProductType,ProductDescription,ProductProduceTime,Stock")] Product product, IFormFile productImage)
        {
            string email = User.Identity.Name;
            product.ProductSellerEmail = email;
            product.ProductSellerName = email;

            if (ValidateProduct(product))
            {
                if (productImage != null)
                {
                    // Ensure the directory exists
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Photo");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Save the image to wwwroot/photos
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + productImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await productImage.CopyToAsync(fileStream);
                    }
                    product.ProductImage = uniqueFileName;
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Log validation errors
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    System.Diagnostics.Debug.WriteLine($"Property: {state.Key}, Error: {error.ErrorMessage}");
                }
            }

            return View(product);
        }

        private bool ValidateProduct(Product product)
        {
            // Create a list to hold validation results
            var validationResults = new List<ValidationResult>();

            // Manually validate the model
            bool isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            // If needed, you can log the validation results
            foreach (var validationResult in validationResults)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {validationResult.ErrorMessage}");
            }

            return isValid;
        }







    }
}