using WebShopFurniture.Models.EntitieDtos.ProductDtos;
using WebShopFurniture.Models.Entities;

namespace WebShopFurniture.ShopFurniture.IServices
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int Id);
        Task<int> AddProductAsync(CreateForProductDto product);
        Task<int> UpdateProductAsync(UpdateForProductDto product);
        Task<int> DeleteProductAsync(int Id);
        Task<List<Product>> GetAvailableProducts();
     }
}
