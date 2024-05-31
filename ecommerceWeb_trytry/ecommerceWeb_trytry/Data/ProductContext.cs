using ecommerceWeb_trytry.Models;
using Microsoft.EntityFrameworkCore;



namespace ecommerceWeb_trytry.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }



        // DbSet<Product> represents the collection of all Product entities in the context.
        // It provides querying and saving capabilities for Product entities.
        public DbSet<Product> Products { get; set; }


        // Override the OnModelCreating method to customize the model creation process.
        // This method is invoked by EF Core when the model for the context is being created.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Product entity to map to a table named "Product" in the database.
            // By default, EF Core would map the entity to a table named "Products" (plural form).
            // This line ensures the table is named "Product" instead.
            modelBuilder.Entity<Product>().ToTable("Product");
       
        }

    }
}
