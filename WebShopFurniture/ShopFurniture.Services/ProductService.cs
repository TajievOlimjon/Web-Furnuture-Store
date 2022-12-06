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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductService(ApplicationContext context,IMapper mapper,IWebHostEnvironment webHost)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHost;
        }
        public async ValueTask<List<Product>> GetAvailableProducts()
        {
            var items=
                await _context.Products
                .Where(x=>x.AvailableProduct==true).ToListAsync();

            return items;
        }
        public async ValueTask<ProductDto> GetProductByIdAsync(int Id)
        {
            var item = await (from p in _context.Products
                              where p.Id==Id
                              select new ProductDto
                              {
                                  Id = p.Id,
                                  ProductName = p.ProductName,
                                  ShortDesc = p.ShortDesc,
                                  FullDesc = p.FullDesc,
                                  date = p.date,
                                  Manafacturer = p.Manafacturer,
                                  FurnitureMadeOf = p.FurnitureMadeOf,
                                  Price = p.Price,
                                  Quantity = p.Quantity,
                                  Image = p.Image,
                                  AvailableProduct = p.AvailableProduct,
                                  CategoryId = p.CategoryId
                              }).FirstOrDefaultAsync();

            if (item == null) return null;

            return item;
        }
        public async ValueTask<List<ProductDto>> GetProductsAsync()
        {
            var products = await (from p in _context.Products
                                  select new ProductDto
                                  {
                                      Id = p.Id,
                                      ProductName=p.ProductName,
                                      ShortDesc=p.ShortDesc,
                                      FullDesc=p.FullDesc,
                                      date=p.date,
                                      Manafacturer=p.Manafacturer,
                                      FurnitureMadeOf=p.FurnitureMadeOf,
                                      Price=p.Price,
                                      Quantity=p.Quantity,
                                      Image=p.Image,
                                      AvailableProduct=p.AvailableProduct,
                                      CategoryId=p.CategoryId
                                  }).ToListAsync();
            
            if (products == null) return new List<ProductDto>();

            return products;
        }
        public async ValueTask<int> UpdateProductAsync(ProductDto product)
        {
            var p = await _context.Products.FindAsync(product.Id);

            if(p==null) return 0;

            if (product.Img!=null)
            {
                p.Image = UpdateProductFile(product.Img);
            }
            else
            {
                p.Image = product.Image;
            }
            p.ProductName = product.ProductName;
            p.ShortDesc = product.ShortDesc;
            p.FullDesc = product.FullDesc;
            p.date = product.date;
            p.Manafacturer = product.Manafacturer;
            p.FurnitureMadeOf = product.FurnitureMadeOf;
            p.Price = product.Price;
            p.Quantity = product.Quantity;
            p.Image = p.Image;
            p.AvailableProduct = product.AvailableProduct;
            p.CategoryId = product.CategoryId;

            var x = await _context.SaveChangesAsync();


            if (x == 0) return 0;
            return x;
        }
        public async ValueTask<int> AddProductAsync(CreateForProductDto product)
        {
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

            DeleteProductFileAsync(product.Image);

            _context.Products.Remove(product);

            var x = await _context.SaveChangesAsync();

            if (x == 0) return 0;
            return x;
        }
        private string AddProductFile(IFormFile file)
        {
            if (file != null)
            {
                var fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Image", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return fileName;
            }
            return null;

        }
        private string UpdateProductFile(IFormFile file)
        {
            string fileName, path;

            if (!file.Equals(null))
            {
                //return new Exception("В коде изменение фото продукта есть ").ToString();
                var fullPath = _webHostEnvironment.WebRootPath + file;
                if (!File.Exists(fullPath))
                {
                    fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
                    path = Path.Combine(_webHostEnvironment.WebRootPath, "Image", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return fileName;
                }
                else if (File.Exists(fullPath))
                return fullPath;

            }
            return null;
        }
        private string DeleteProductFileAsync(string file)
        {
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image", file);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
                return "Deleted file";
            }
            return "No deleted file";
                 
        }

      
    }
}
