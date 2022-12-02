using Microsoft.EntityFrameworkCore;
using WebShopFurniture.Data.DataContextDb;
using WebShopFurniture.Models.Entities;
using WebShopFurniture.ShopFurniture.IServices;

namespace WebShopFurniture.ShopFurniture.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly ApplicationContext _context;
        public CustomerService(ApplicationContext context)
        {
            _context = context;
        }

        public async ValueTask<List<Customer>> GetCustomers()
        {
            var customers = await (from customer in _context.Customers
                                   select customer
                                 ).ToListAsync();
            return customers;
        }
    }
}
