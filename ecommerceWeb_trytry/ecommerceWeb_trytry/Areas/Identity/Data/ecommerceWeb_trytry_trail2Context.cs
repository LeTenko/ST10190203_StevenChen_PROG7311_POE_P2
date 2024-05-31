using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ecommerceWeb_trytry_trail2.Areas.Identity.Data;

public class ecommerceWeb_trytry_trail2Context : IdentityDbContext<IdentityUser>
{
    public ecommerceWeb_trytry_trail2Context(DbContextOptions<ecommerceWeb_trytry_trail2Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
