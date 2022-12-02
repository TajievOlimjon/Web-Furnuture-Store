using Microsoft.AspNetCore.Mvc;
using WebShopFurniture.ShopFurniture.IServices;

namespace WebShopFurniture.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }
        public async ValueTask<IActionResult> Index()
        {
            var customers = await _service.GetCustomers();

            return View(customers);
        }
    }
}
