using ecommerceWeb_trytry.Data;
using ecommerceWeb_trytry.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerceWeb_trytry_trail2.Services
{
    public class UserStoreService : IUserStoreService
    {


        private readonly ProductContext _context;

        public UserStoreService(ProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsBySellerEmailAsync(string email)
        {
            return await _context.Products
                .Where(p => p.ProductSellerEmail == email)
                .ToListAsync();
        }
    }
}
