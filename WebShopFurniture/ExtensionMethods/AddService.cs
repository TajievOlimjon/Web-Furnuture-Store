using AutoMapper;
using WebShopFurniture.Entities.Mappers;
using WebShopFurniture.ShopFurniture.IServices;
using WebShopFurniture.ShopFurniture.Services;

namespace WebShopFurniture.ExtensionMethods
{
    public static class AddService
    {
        public static IServiceCollection AddServicesToContainer(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
           // services.AddTransient<ICartService,CartService>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<ICustomerService,CustomerService>();

            services.AddAutoMapper(typeof(MapperEntities));

            return services;
        }
    }
}
