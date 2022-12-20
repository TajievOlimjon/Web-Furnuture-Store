using Microsoft.AspNetCore.Mvc;
using WebShopFurniture.Models.EntitieDtos.ProductDtos;
using WebShopFurniture.ShopFurniture.IServices;

namespace WebShopFurniture.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {  
        private readonly IProductService _service;
        private readonly ICategoryService _serviceCat;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService service,
            ICategoryService serviceCat,
            ILogger<ProductController> logger)
        {
            _service = service;
            _serviceCat = serviceCat;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var products=
                await _service.GetProductsAsync();

            return View(products);
        }

        public async Task<ActionResult<GetProductDto>> Details(int id)
        {
            var product = await _service.GetProductByIdAsync(id);

            var p = new GetProductDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ShortDesc = product.ShortDesc,
                FullDesc = product.FullDesc,
                date = product.date,
                Manafacturer = product.Manafacturer,
                FurnitureMadeOf = product.FurnitureMadeOf,
                Price = product.Price,
                Quantity = product.Quantity,
                Image = product.Image,
                AvailableProduct = product.AvailableProduct,
                CategoryId = product.CategoryId
            };

            return View(p);
        }

        public async Task<IActionResult> Create()
        {
            var category =
                await _serviceCat.GetCategoriesAsync();

            ViewBag.Categories = category;
            return View(new CreateForProductDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateForProductDto dto)
        {
            var category =
                await _serviceCat.GetCategoriesAsync();
            ViewBag.Categories = category;

            dto.CreateAt = DateTimeOffset.UtcNow;

            try
            { 
                if (ModelState.IsValid)
                {
                    await _service.AddProductAsync(dto);
                    return RedirectToAction("Index");
                }
                return View(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ошибка в коде !");

                throw new Exception(ex.Message);
            }
        }

       
        public async ValueTask<IActionResult> Edit(int id)
        {
            var category =
               await _serviceCat.GetCategoriesAsync();
            ViewBag.Categories = category;

            var product =
                await _service.GetProductByIdAsync(id);

            var p = new UpdateForProductDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ShortDesc = product.ShortDesc,
                FullDesc = product.FullDesc,
                date = product.date,
                Manafacturer = product.Manafacturer,
                FurnitureMadeOf = product.FurnitureMadeOf,
                Price = product.Price,
                Quantity = product.Quantity,
                Image = product.Image,
                AvailableProduct = product.AvailableProduct,
                CategoryId = product.CategoryId
            };

            return View(p);
        }

        [HttpPost]
        public async ValueTask<IActionResult> Edit(UpdateForProductDto dto)
        {
            //try
            //{
                var category =
               await _serviceCat.GetCategoriesAsync();
                ViewBag.Categories = category;

                if (ModelState.IsValid)
                {   
                    await  _service.UpdateProductAsync(dto);
                    return RedirectToAction("Index");
                }
               
                return View(dto);
            //}
            //catch (Exception ex)
            //{   
            //    _logger.LogError(ex.Message.ToString(), "В коде ест ошибка !");
            //     throw new Exception("",ex.InnerException);
            //}
        }
        public async ValueTask<IActionResult> Delete(int id)
        {
            var product =
                await _service.GetProductByIdAsync(id);

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async ValueTask<IActionResult?> DeleteConfirmed(int id)
        {
            if (id.Equals(null)) return null;

            await _service.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }
    }
}
