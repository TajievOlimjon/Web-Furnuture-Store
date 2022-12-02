using Microsoft.EntityFrameworkCore;
using WebShopFurniture.Data.DataContextDb;
using WebShopFurniture.Models.Entities;
using WebShopFurniture.ShopFurniture.IServices;

namespace WebShopFurniture.ShopFurniture.Services
{
    public class CartService:ICartService
    {
        private readonly ApplicationContext _context;
        public string CartId { get; set; }
        public CartService(ApplicationContext context)
        {
            _context = context;
        }
        public static CartService GetShopCart(IServiceProvider service)
        {
            ISession? session =
                service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            ApplicationContext? context = service.GetService<ApplicationContext>();
            string CartId = session.GetString("cartId") ?? Guid.NewGuid().ToString();
            session.SetString("cartId", CartId);

            return new CartService(context) { CartId = CartId };
        }
        public async ValueTask<int> AddToCart(int Id)
        { 
            try
            {
                var item = 
                    await _context.Products.FindAsync(Id);

                if (item == null) return 0;

                var cart = new Cart
                {
                    ProductId = item.Id,
                    CartId = CartId,
                    Quantity = item.Quantity
                };

                await _context.Carts.AddAsync(cart);

                var x = await _context.SaveChangesAsync();

                if (x == 0) return 0;
                return x;
            }
            catch (Exception ex)
            {
                var massege = "В сервисе корзины что то не так ! , проверте ?";
                throw new Exception(massege,ex.InnerException);
            }
           

        }

        public async ValueTask<List<Cart>> GetCarts()
        {
            try
            {
                var carts = await (from c in _context.Carts
                                   where c.CartId == CartId
                                   join p in _context.Products on c.ProductId equals p.Id
                                   select c
                                 ).ToListAsync();
                return carts;
            }
            catch (Exception ex)
            {

                var massege = "В сервисе корзины что то не так ! , проверте ?";
                throw new Exception(massege, ex.InnerException);
            }
            
                 
        }

    }
}
