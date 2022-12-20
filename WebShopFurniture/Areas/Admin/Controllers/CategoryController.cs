using Microsoft.AspNetCore.Mvc;
using WebShopFurniture.Models.EntitieDtos.CategoryDtos;
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
        [HttpGet]
        public async ValueTask<IActionResult> Index()
        {
            var categories = await _service.GetCategoriesAsync();

            return View(categories);
        }
        [HttpGet]
        public async ValueTask<ActionResult<CategoryDto>> Details(int id)
        {
            var category = await _service.GetCategoryById(id);

            return View(category);
        }
        [HttpGet]
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
        [HttpGet]
        public async ValueTask<IActionResult> Edit(int Id)
        {
            var item =
                await _service.GetCategoryById(Id);

            return View(item);
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
                throw new /*Exception(ex.Message);*/ InvalidOperationException("",ex);
            }
        }
        [HttpGet]
        public async ValueTask<IActionResult> Delete(int Id)
        {
            var item =
                await _service.GetCategoryById(Id);

            return View(item);
        }

        [HttpPost,ActionName("Delete")]
        public async ValueTask<IActionResult?> DeleteComfirmed(int id)
        {
            if (id.Equals(null)) return null;

            await _service.DeleteCategoryAsync(id);

            return RedirectToAction("Index");
        }
    }
}


