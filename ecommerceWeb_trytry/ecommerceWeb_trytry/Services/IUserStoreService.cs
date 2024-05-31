using ecommerceWeb_trytry.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecommerceWeb_trytry_trail2.Services
{
    public interface IUserStoreService
    {
        Task<IEnumerable<Product>> GetProductsBySellerEmailAsync(string email);
    }
}
