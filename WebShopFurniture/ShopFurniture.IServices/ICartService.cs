using WebShopFurniture.Models.EntitieDtos.ProductDtos;
using WebShopFurniture.Models.Entities;
using WebShopFurniture.ShopFurniture.Services;

namespace WebShopFurniture.ShopFurniture.IServices
{
    public interface ICartService
    {
        ValueTask<int> AddToCart(int Id);
        ValueTask<List<Cart>> GetCarts();
        CartService GetShopCart(IServiceProvider service);
    }
}
