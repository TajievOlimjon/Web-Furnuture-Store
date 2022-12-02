using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebShopFurniture.Data.DataContextDb;
using WebShopFurniture.Models.EntitieDtos.ProductDtos;
using WebShopFurniture.Models.Entities;
using WebShopFurniture.ShopFurniture.IServices;

namespace WebShopFurniture.ShopFurniture.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async ValueTask<int> AddCategoryAsync(CategoryDto category)
        {
            var mappedCategory = _mapper.Map<Category>(category);

            await _context.Categories.AddAsync(mappedCategory);

            var x = await _context.SaveChangesAsync();

            if (x.Equals(0)) return 0;
            return x;
        }

        public async ValueTask<int> DeleteCategoryAsync(int Id)
        {
            var category =
                await _context.Categories.FindAsync(Id);

            if (category == null) return 0;

            _context.Categories.Remove(category);

            var x= await _context.SaveChangesAsync();

            if (x.Equals(0)) return 0;
            return x;
        }

        public async ValueTask<List<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async ValueTask<Category> GetCategoryById(int Id)
        {
            var x= await _context.Categories.FindAsync(Id);

            if (x == null) return new Category();

            return x;
        }

        public async ValueTask<int> UpdateCategoryAsync(CategoryDto category)
        {
            var c = await _context.Categories.FindAsync(category.Id);
            if (c == null) return 0;

            c.CategoryName = category.CategoryName;
            c.ShortDesc = category.ShortDesc;
            c.FullDesc = category.FullDesc;

            var x = await _context.SaveChangesAsync();

            if (x.Equals(0)) return 0;
            return x;
        }
    }
}
