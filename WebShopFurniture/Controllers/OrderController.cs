using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebShopFurniture.Models.Entities;
using WebShopFurniture.ShopFurniture.IServices;

namespace WebShopFurniture.Controllers
{
    public class OrderController:Controller
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }

        public IActionResult AddToOrder()
        {
            return View(new Customer());
        }
        [HttpPost]
        public async ValueTask<IActionResult> AddToOrder(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   var x= await _service.AddToOrder(customer);

                    if (x != 0) return RedirectToAction("Result");

                    return View(customer);
                }
                return View(HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public IActionResult Result()
        {
            return View();
        }
    }
}
