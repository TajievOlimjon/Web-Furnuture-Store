using AutoMapper;
using WebShopFurniture.Models.EntitieDtos.ProductDtos;
using WebShopFurniture.Models.Entities;

namespace WebShopFurniture.Entities.Mappers
{
    public class MapperEntities:Profile
    {
        public MapperEntities()
        {
            CreateMap<ProductDto, Product>().ReverseMap()
                .ForMember(dest=>dest.Img,act=>act.MapFrom(src=>src.Image));
            CreateMap<GetProductDto, Product>().ReverseMap();
            CreateMap<CategoryDto,Category>().ReverseMap();
        }
    }
}
