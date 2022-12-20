namespace WebShopFurniture.Models.EntitieDtos.ProductDtos
{
    public class UpdateForProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ShortDesc { get; set; }
        public string FullDesc { get; set; }
        public DateTimeOffset date { get; set; } 
        public string FurnitureMadeOf { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public IFormFile? Img  { get; set; }
        public string Manafacturer { get; set; }
        public bool AvailableProduct { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public int CategoryId { get; set; }
    }
}
