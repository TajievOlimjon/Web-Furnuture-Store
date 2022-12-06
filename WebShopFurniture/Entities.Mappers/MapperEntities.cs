using AutoMapper;
using WebShopFurniture.Models.EntitieDtos.ProductDtos;
using WebShopFurniture.Models.Entities;

namespace WebShopFurniture.Entities.Mappers
{
    public class MapperEntities:Profile
    {
        public MapperEntities()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CreateForProductDto, Product>().ReverseMap();
            CreateMap<CategoryDto,Category>().ReverseMap();
        }
    }
}
