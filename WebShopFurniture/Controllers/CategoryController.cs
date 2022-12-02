using Microsoft.AspNetCore.Mvc;
using WebShopFurniture.Models.EntitieDtos.ProductDtos;
using WebShopFurniture.Models.Entities;
using WebShopFurniture.ShopFurniture.IServices;

namespace WebShopFurniture.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        public async ValueTask<IActionResult> Index()
        {
            var categories = await _service.GetCategoriesAsync();

            return View(categories);
        }

    }
}


