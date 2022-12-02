using WebShopFurniture.Models.Entities;

namespace WebShopFurniture.ShopFurniture.IServices
{
    public interface IOrderService
    {
        ValueTask<List<Order>> GetOrders();
        ValueTask<int> AddToOrder(Customer customer);
    }
}
