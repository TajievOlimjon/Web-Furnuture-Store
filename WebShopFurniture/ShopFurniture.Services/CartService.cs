using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebShopFurniture.Data.DataContextDb;
using WebShopFurniture.Models.Entities;
using WebShopFurniture.ShopFurniture.IServices;

namespace WebShopFurniture.ShopFurniture.Services
{
    public class CartService
    {
        private readonly ApplicationContext _context;
        public string CartId { get; set; }
        public readonly IProductService _productService;
        public readonly IMapper _mapper;
        public CartService(ApplicationContext context, IProductService productService,IMapper mapper)
        {
            _context = context;
            _productService = productService;
            _mapper = mapper;
        }
        public CartService(ApplicationContext context)
        {
            _context = context;
        }

        public static  CartService GetShopCart(IServiceProvider service)
        {
            ISession? session =
                service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<ApplicationContext>();
            string CartId = session.GetString("cartId") ?? Guid.NewGuid().ToString();
            session.SetString("cartId", CartId);

            return new CartService(context) { CartId = CartId };
        }
        public async ValueTask<int> AddAllToCart(int Id)
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

                var k =item.Quantity-cart.Quantity;
                item.Quantity = k;

                await UpdateProduct(item);

                if (x == 0) return 0;
                return x;
            }
            catch (Exception ex)
            {
                var massege = "В сервисе корзины что то не так ! , проверте ?";
                throw new Exception(massege, ex);
            }
        }
        public async Task<int> AddToCart(int Id,int q)
        { 
            try
            {
                var item =
                    await _productService.GetProductByIdAsync(Id);
                   /*await _context.Products.FindAsync(Id);*/

                if (item == null) return 0;


                var cart = new Cart
                {
                    ProductId = item.Id,
                    CartId = CartId,
                    Quantity = q
                };

                await _context.Carts.AddAsync(cart);

                var x = await _context.SaveChangesAsync();

                var k = item.Quantity - cart.Quantity;
                item.Quantity = k;


                await _productService.UpdateProductAsync(item);
              // await UpdateProduct(item);

                if (x == 0) return 0;
                return x;
            }
            catch (Exception ex)
            {
                var massege = 
                    " В сервисе корзины что то" +
                    " пошло не так ! , проверте ? ";
                throw new Exception(massege,ex);
            }
           

        }

        public async ValueTask<List<Cart>> GetCarts()
        {
            try
            {
                /*var carts = await (from c in _context.Carts
                                   where c.CartId == CartId
                                   join p in _context.Products on c.ProductId equals p.Id
                                   select c
                                 ).ToListAsync();*/
                var carts = await _context.Carts
                           .Where(x => x.CartId == CartId)
                           .Include(x => x.Product)
                           .ToListAsync();
                return carts;
            }
            catch (Exception ex)
            {

                var massege = "В сервисе корзины что то не так ! , проверте ?";
                throw new Exception(massege, ex);
            }
            
                 
        }

        public async ValueTask<List<Cart>> GetAllProductsFromCarts()
        {
            var carts = await _context.Carts
                          .Include(x => x.Product)
                          .ToListAsync();
            return carts;
        }
        
        private async ValueTask<int> UpdateProduct(Product product )
        {
            var item = 
                await _context.Products.FindAsync(product.Id);

            if (item == null) return 0;

            /*_context.Attach(item);
            _context.Entry(item).State = EntityState.Modified;*/
            item.ProductName = product.ProductName;
            item.ShortDesc = product.ShortDesc;
            item.FullDesc = product.FullDesc;
            item.date = product.date;
            item.Manafacturer = product.Manafacturer;
            item.FurnitureMadeOf = product.FurnitureMadeOf;
            item.Price = product.Price;
            item.Quantity = product.Quantity;
            item.Image = product.Image;
            item.AvailableProduct = product.AvailableProduct;
            item.CategoryId = product.CategoryId;

            var x=await  _context.SaveChangesAsync();

            if (x == 0) return 0;
            return x;
        }
    }
}
