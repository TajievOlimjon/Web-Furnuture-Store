using Microsoft.EntityFrameworkCore;
using WebShopFurniture.Data.DataContextDb;
using WebShopFurniture.Models.Entities;
using WebShopFurniture.ShopFurniture.IServices;

namespace WebShopFurniture.ShopFurniture.Services
{
    public class OrderService:IOrderService
    {
        private readonly ApplicationContext _context;
        private readonly ICartService _cartService;
        public OrderService(ApplicationContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public async ValueTask<int> AddToOrder(Customer customer)
        {
            try
            {
                customer.OrderTime = DateTimeOffset.UtcNow;

                await _context.Customers.AddAsync(customer);

                await _context.SaveChangesAsync();

                var c =
                    await _context.Customers.
                    FirstOrDefaultAsync(x =>
                       x.Email.Equals(customer.Email)
                    );

                var carts = await _cartService.GetCarts();

                foreach (var item in carts)
                {
                    if (item.Equals(null))
                    {
                        var order = new Order
                        {
                            CustomerId = c.Id,
                            ProductId = item.Product.Id,
                            Quantity = item.Quantity,
                            OrderDate=DateTimeOffset.UtcNow
                        };
                        await _context.Orders.AddRangeAsync(order);
                    }
                }
               var x= await _context.SaveChangesAsync();

                if(!x.Equals(0)) return  x;

                _context.Customers.Remove(c);
                await _context.SaveChangesAsync();

                return 0;


            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
            
        }

        public async ValueTask<List<Order>> GetOrders()
        {
            var orders = await (from order in _context.Orders
                                join p in _context.Products on order.ProductId equals p.Id
                                join c in _context.Customers on order.CustomerId equals c.Id
                                select order
                                ).ToListAsync();
            return orders;
        }
    }
}
