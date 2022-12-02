using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using WebShopFurniture.Data.DataContextDb;
using WebShopFurniture.Models.EntitieDtos.ProductDtos;
using WebShopFurniture.Models.Entities;
using WebShopFurniture.ShopFurniture.IServices;

namespace WebShopFurniture.ShopFurniture.Services
{
    public class ProductService:IProductService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public ProductService(ApplicationContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async ValueTask<int> AddProductAsync(ProductDto product)
        {
            product.CreateAt = DateTimeOffset.UtcNow;
            
            var productDto = _mapper.Map<Product>(product);
            productDto.Image = AddProductFile(product.Image);
            await _context.Products.AddAsync(productDto);

            var x = await _context.SaveChangesAsync();
            if (x != 0)
            {
                return x;
            }
            return 0;
        }

        public async ValueTask<int> DeleteProductAsync(int Id)
        {
            var product = 
                await _context.Products.FindAsync(Id);

            if (product == null) return 0;

            _context.Products.Remove(product);

            var x = await _context.SaveChangesAsync();

            if (x == 0) return 0;
            return x;
        }

        public async ValueTask<List<Product>> GetAvailableProducts()
        {
            var items=
                await _context.Products
                .Where(x=>x.AvailableProduct==true).ToListAsync();

            return items;
        }

        public async ValueTask<GetProductDto> GetProductByIdAsync(int Id)
        {
            var product =
                await _context.Products.FindAsync(Id);

            var productDto = _mapper.Map<GetProductDto>(product);

            if (productDto == null) return null;

            return productDto;
        }

        public async ValueTask<List<ProductDto>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();

            var p = _mapper.Map<List<ProductDto>>(products);

            if (p == null) return new List<ProductDto>();

            return p;
        }

        public async ValueTask<int> UpdateProductAsync(ProductDto product)
        {
            var p = await _context.Products.FindAsync(product.Id);

            if(p==null) return 0;

            var productDto = _mapper.Map<Product>(product);

            productDto.Image = UpdateProductFile(product.Image);

            _context.Entry(productDto).State = EntityState.Modified;

            var x = await _context.SaveChangesAsync();

            if (x == 0) return 0;
            return x;
        }

        private string AddProductFile(IFormFile file)
        {
            var fileName = Guid.NewGuid() + "_" + Path.GetFileName(product.Image.FileName);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await product.Image.CopyToAsync(stream);
            }

        }
        private string UpdateProductFile(IFormFile file)
        {
            return null;
        }
    }
}
