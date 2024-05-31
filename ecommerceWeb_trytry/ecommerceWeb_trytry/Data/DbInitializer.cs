
using ecommerceWeb_trytry.Data;
using ecommerceWeb_trytry.Models;
using ecommerceWeb_trytry_trail2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;
using System;
using System.Linq;




namespace ecommerceWeb_trytry.Data
{
    public static class Dbinitializer
    {

        public static void ProductInitialize(ProductContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                Console.WriteLine("DB IS FULLLL");
                return; // DB has been seeded
            }

            var products = new Product[]
            {
                new Product
                {
                   
                    ProductSellerEmail = "Farmer1@farmer.com",
                    ProductSellerName = "Steve",
                    ProductName = "Diamond pickaxe",
                    ProductPrice = 101.50,
                    ProductImage = "enchanted_diamond_pik.png",
                    ProductType = "Type 1", // Add an appropriate value for ProductType
                    ProductDescription = "enchanted, fully functional pickaxe",
                    ProductProduceTime = new DateTime(2023, 1, 15),
                    Stock = 10
                },
                new Product
                {
                  
                    ProductSellerEmail = "Farmer2@farmer.com",
                    ProductSellerName = "Villager",
                    ProductName = "Emerald hoe",
                    ProductPrice = 999999,
                    ProductImage = "emerald_hoe.png",
                    ProductType = "Type2", // Add an appropriate value for ProductType
                    ProductDescription = "hUmm , huMmm , HummMMm",
                    ProductProduceTime = new DateTime(2023, 2, 20),
                    Stock = 20
                },
                new Product
                {
                   
                    ProductSellerEmail = "Farmer1@farmer.com",
                    ProductSellerName ="Steve",
                    ProductName = "Wood Axe",
                    ProductPrice = 203.50,
                    ProductImage = "niceAXE.png",
                    ProductType = "Type3", // Add an appropriate value for ProductType
                    ProductDescription = "good for cutting wood. safe to have it around",
                    ProductProduceTime = new DateTime(2023, 3, 25),
                    Stock = 30
                },
                new Product
                {
                   
                    ProductSellerEmail = "Farmer3@farmer.com",
                    ProductSellerName = "DragonSlayer420",
                    ProductName = "Chainsaw",
                    ProductPrice = 333.50,
                    ProductImage = "dragonkilla.png",
                    ProductType = "Type4", // Add an appropriate value for ProductType
                    ProductDescription = "HANDY item for any type of close range actions",
                    ProductProduceTime = new DateTime(2023, 4, 30),
                    Stock = 40
                },
                new Product
                {

                    ProductSellerEmail = "Farmer1@farmer.com",
                    ProductSellerName ="Steve",
                    ProductName = "Fruit knife",
                    ProductPrice = 88.50,
                    ProductImage = "styelish_fruit_knife.png",
                    ProductType = "Type3", // Add an appropriate value for ProductType
                    ProductDescription = "Perfect for carving your holiday turkey or dispatching overcooked casseroles",
                    ProductProduceTime = new DateTime(2023, 3, 25),
                    Stock = 30
                }
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }

            context.SaveChanges();
        }


        //================================================================================================//
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roles = new List<string> { "Employee", "Farmer" };

            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
        }



        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var users = new[]
            {
            new { Email = "Farmer1@farmer.com", Password = "Farmer@123", Role = "Farmer" },
            new { Email = "Farmer2@farmer.com", Password = "Farmer@123", Role = "Farmer" },
            new { Email = "Farmer3@farmer.com", Password = "Farmer@123", Role = "Farmer" },
            new { Email = "Admin@admin.com", Password = "Admin@123", Role = "Employee" }
        };

            foreach (var user in users)
            {
                await SeedUserAsync(serviceProvider, user.Email, user.Password, user.Role);
            }
        }

        private static async Task SeedUserAsync(IServiceProvider serviceProvider, string email, string password, string role)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Ensure the role exists
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                // Check if the user already exists
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser
                    {
                      UserName=email,
                        Email = email,
                        EmailConfirmed = true // Email confirmed
                       
                     

                    };

                    // Create the user
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        // Assign the role
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }
        }













    }



}




