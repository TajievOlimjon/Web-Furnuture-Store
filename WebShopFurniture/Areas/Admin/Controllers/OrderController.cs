using Microsoft.AspNetCore.Mvc;
using WebShopFurniture.Models.Entities;
using WebShopFurniture.ShopFurniture.IServices;

namespace WebShopFurniture.Areas.Admin.Controllers
{
    public class OrderController:BaseController
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }

        public async ValueTask<IActionResult> Index()
        {
            var orders = await _service.GetOrders();

            return View(orders);
        }

    }
}
