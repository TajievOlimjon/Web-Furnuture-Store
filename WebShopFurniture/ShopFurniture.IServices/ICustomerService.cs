using WebShopFurniture.Models.Entities;

namespace WebShopFurniture.ShopFurniture.IServices
{
    public interface ICustomerService
    {
        ValueTask<List<Customer>> GetCustomers();
    }
}
