using WebShopFurniture.Models.EntitieDtos.ProductDtos;
using WebShopFurniture.Models.Entities;

namespace WebShopFurniture.ShopFurniture.IServices
{
    public interface IProductService
    {
        ValueTask<List<ProductDto>> GetProductsAsync();
        ValueTask<GetProductDto> GetProductByIdAsync(int Id);
        ValueTask<int> AddProductAsync(ProductDto product);
        ValueTask<int> UpdateProductAsync(ProductDto product);
        ValueTask<int> DeleteProductAsync(int Id);
        ValueTask<List<Product>> GetAvailableProducts();
     }
}
