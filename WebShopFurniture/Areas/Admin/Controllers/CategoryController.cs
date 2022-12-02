using Microsoft.AspNetCore.Mvc;
using WebShopFurniture.Models.EntitieDtos.ProductDtos;
using WebShopFurniture.Models.Entities;
using WebShopFurniture.ShopFurniture.IServices;

namespace WebShopFurniture.Areas.Admin.Controllers
{
    public class CategoryController:BaseController
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

        public async ValueTask<ActionResult<Category>> Details(int id)
        {
            var category = await _service.GetCategoryById(id);

            return View(category);
        }

        public IActionResult Create()
        {
            return View(new CategoryDto());
        }

        [HttpPost]
        public async ValueTask<IActionResult> Create(CategoryDto category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.AddCategoryAsync(category);
                    return RedirectToAction(nameof(Index));
                }
                return View(category);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async ValueTask<IActionResult> Edit(int Id)
        {
            return View(await Details(Id));
        }

        [HttpPost]
        public async ValueTask<IActionResult> Edit(CategoryDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.UpdateCategoryAsync(dto);
                    return RedirectToAction("Index");
                }
                return View(dto);
            }
            catch (Exception ex)
            {
              //  _logger.LogError(ex.Message, "В коде ест ошибка !");
                throw new Exception(ex.Message);
            }
        }
        public async ValueTask<IActionResult> Delete(int id)
        {
            return View(await Details(id));
        }

        [HttpPost]
        public async ValueTask<IActionResult?> Delete(int id, byte i = 0)
        {
            if (id.Equals(null)) return null;

            await _service.DeleteCategoryAsync(id);

            return RedirectToAction("Index");
        }
    }
}


