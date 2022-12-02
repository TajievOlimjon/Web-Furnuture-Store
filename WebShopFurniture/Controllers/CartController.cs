using Microsoft.AspNetCore.Mvc;
using WebShopFurniture.ShopFurniture.IServices;

namespace WebShopFurniture.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _service;
        private readonly ILogger<CartController> _logger;
        private readonly IProductService _productService;

        public CartController(ICartService service, ILogger<CartController> logger,IProductService productService)
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

        public async ValueTask<IActionResult> AddToCart(int Id)
        {
            try
            {

                if (!Id.Equals(null))
                {
                
                    await _service.AddToCart(Id);
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
