using Microsoft.AspNetCore.Mvc;
using WebShopFurniture.ShopFurniture.IServices;
using WebShopFurniture.ShopFurniture.Services;

namespace WebShopFurniture.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _service;
        private readonly ILogger<CartController> _logger;
        private readonly IProductService _productService;

        public CartController(CartService service, ILogger<CartController> logger,IProductService productService)
        {
            _service = service;
            _logger = logger;
            _productService = productService;
        }
        public async ValueTask<ActionResult> Index()
        {
            var carts = await _service.GetCarts();

            return View(carts);
        }
        [HttpGet]
        public async ValueTask<IActionResult> AddAllToCart(int Id)
        {
            try
            {

                if (!Id.Equals(null))
                {

                    await _service.AddAllToCart(Id);
                    return RedirectToAction(nameof(Index));
                }

                return View(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async ValueTask<IActionResult> AddToCart(int Id)
        {
            var item =
                await _productService.GetProductByIdAsync(Id);
            item.Quantity = 0;
            return View(item);
        }
        [HttpPost]
        public async ValueTask<IActionResult> AddToCart(int Id,int quantity)
        {
            try
            {

                if (!Id.Equals(null))
                {
                
                    await _service.AddToCart(Id,quantity);
                    return RedirectToAction(nameof(Index));
                }
                
                return View(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
