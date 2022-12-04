using Microsoft.EntityFrameworkCore;
using WebShopFurniture.Data.DataContextDb;
using WebShopFurniture.ExtensionMethods;
using WebShopFurniture.ShopFurniture.IServices;
using WebShopFurniture.ShopFurniture.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationContext>(o =>
          o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddServicesToContainer();

/*ICartService cartService = null;*/ 
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => CartService.GetShopCart(sp));

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddMvc();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context =
        scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Admin",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
