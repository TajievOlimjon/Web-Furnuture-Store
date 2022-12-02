using Microsoft.AspNetCore.Mvc;
using WebShopFurniture.Models.EntitieDtos.ProductDtos;
using WebShopFurniture.ShopFurniture.IServices;

namespace WebShopFurniture.Controllers
{
    public class ProductController : Controller
    {  
        private readonly IProductService _service;
        private ILogger<ProductController> _logger;
        public ProductController(IProductService service,ILogger<ProductController> logger)
        {
            _service = service;
            _logger = logger;
        }
        public async ValueTask<IActionResult> Index()
        {
            var products=
                await _service.GetProductsAsync();

            return View(products);
        }

        public async ValueTask<ActionResult<GetProductDto>> Details(int id)
        {
             var product = await _service.GetProductByIdAsync(id);

                 return View(product);
        }

        public async ValueTask<IActionResult> GetAvailableProducts()
        {
            var items = await _service.GetAvailableProducts();

            return View(items);
        }
    }
}
