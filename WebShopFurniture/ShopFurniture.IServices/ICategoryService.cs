using WebShopFurniture.Models.EntitieDtos.ProductDtos;
using WebShopFurniture.Models.Entities;

namespace WebShopFurniture.ShopFurniture.IServices
{
    public interface ICategoryService
    {
        ValueTask<List<Category>> GetCategoriesAsync();
        ValueTask<CategoryDto> GetCategoryById(int Id);
        ValueTask<int> AddCategoryAsync(CategoryDto category);
        ValueTask<int> UpdateCategoryAsync(CategoryDto category);
        ValueTask<int> DeleteCategoryAsync(int Id);
    }
}
