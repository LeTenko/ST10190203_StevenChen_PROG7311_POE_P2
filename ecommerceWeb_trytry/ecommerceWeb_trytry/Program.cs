using ecommerceWeb_trytry.Data;
using ecommerceWeb_trytry.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ecommerceWeb_trytry_trail2.Areas.Identity.Data;
using ecommerceWeb_trytry_trail2.Services;




//============================//

var builder = WebApplication.CreateBuilder(args);

//new
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ecommerceWeb_trytry_trail2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<ecommerceWeb_trytry_trail2Context>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();//helps resolving problems with database


// Add services to the container.

builder.Services.AddScoped<IUserStoreService, UserStoreService>();








builder.Services.AddHttpContextAccessor(); //for cart service
builder.Services.AddTransient<CartService>(); //for cart service
builder.Services.AddDistributedMemoryCache(); //for cart service
builder.Services.AddSession(options=>
{ options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    
}); //for cart service






// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



//new
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {

        // add-migration InitialProductContext -context ProductContext
        // update-database -context ProductContext
        // ProductId = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),



        // add-migration InitialLoginPortal_Context -context ecommerceWeb_trytry_trail2Context
        // update-database -context ecommerceWeb_trytry_trail2Context

        var productContext = services.GetRequiredService<ProductContext>();
        productContext.Database.Migrate();
        Dbinitializer.ProductInitialize(productContext);

        var loginPortalContext = services.GetRequiredService<ecommerceWeb_trytry_trail2Context>();
        loginPortalContext.Database.Migrate();
       
        await Dbinitializer.SeedRolesAsync(app.Services);
        await Dbinitializer.SeedUsersAsync(app.Services);



    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred creating the DB.");
        Console.WriteLine($"An error occurred: {ex.Message}");
        Console.WriteLine(ex.StackTrace);
    }
}













// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();  //optional
    }
    else
    {
        app.UseMigrationsEndPoint();  //this for login adjustments
    }

app.UseSession(); //for cart service

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//new
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

////applying migration for user identity database
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<EcommerceIdentityContextTryTry>();
//    dbContext.Database.Migrate();
//}




//for login portals
app.MapRazorPages();

app.Run();
