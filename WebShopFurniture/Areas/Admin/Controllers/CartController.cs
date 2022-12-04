using Microsoft.AspNetCore.Mvc;
using WebShopFurniture.ShopFurniture.Services;

namespace WebShopFurniture.Areas.Admin.Controllers
{
    public class CartController : BaseController
    {
        private readonly CartService _service;
        private readonly ILogger<CartController> _logger;

        public CartController(CartService service, ILogger<CartController> logger)
        {
            _service = service;
            _logger = logger;
        }
        public async ValueTask<ActionResult> Index()
        {
            var carts = await _service.GetAllProductsFromCarts();

            return View(carts);
        }

       
    }
}
