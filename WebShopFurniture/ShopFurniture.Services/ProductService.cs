using AutoMapper;
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
        private readonly IFileService _fileService;
        public ProductService(ApplicationContext context,IMapper mapper,IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService=fileService;
        }
        public async Task<List<Product>> GetAvailableProducts()
        {
            var items=
                 await _context.Products
                .Where(x=>x.AvailableProduct==true)
                .ToListAsync();

            return items;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var item = await _context.Products.FindAsync(id);

            if (item == null) return null;

            return item;
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            var products =
                await  _context.Products.ToListAsync();
            
            if (products == null) return new List<Product>();

            return products;
        }
        public async Task<int> UpdateProductAsync(UpdateForProductDto product)
        {
            var p = 
                await _context.Products.FindAsync(product.Id);

            if (p == null) return 0;

            string imagePath;

            if (product.Img != null)
            {
                imagePath =_fileService.UpdateFile(product.Img);
            }
            else
            {
                imagePath = product.Image;
            }
            
            p.ProductName = product.ProductName;
            p.ShortDesc = product.ShortDesc;
            p.FullDesc = product.FullDesc;
            p.date = product.date;
            p.Manafacturer = product.Manafacturer;
            p.FurnitureMadeOf = product.FurnitureMadeOf;
            p.Price = product.Price;
            p.Quantity = product.Quantity;
            p.Image = imagePath;
            p.AvailableProduct = product.AvailableProduct;
            p.CategoryId = product.CategoryId;

            var x = await _context.SaveChangesAsync();


            if (x == 0) return 0;
            return x;
        }
        public async Task<int> AddProductAsync(CreateForProductDto product)
        {
            var productDto = _mapper.Map<Product>(product);

            productDto.Image = _fileService.AddFile(product.Image);

            await _context.Products.AddAsync(productDto);

            var x = await _context.SaveChangesAsync();

            if (x != 0)
            {
                return x;
            }
            return 0;
        }
        public async Task<int> DeleteProductAsync(int Id)
        {
            var product =
                await _context.Products.FindAsync(Id);

            if (product == null) return 0;

            _fileService.DeleteFile(product.Image);

            _context.Products.Remove(product);

            var x = await _context.SaveChangesAsync();

            if (x == 0) return 0;
            return x;
        }
    }
}
