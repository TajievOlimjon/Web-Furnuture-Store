namespace WebShopFurniture.Models.EntitieDtos.ProductDtos
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ShortDesc { get; set; }
        public string FullDesc { get; set; }
        public DateTimeOffset date { get; set; }  // дата производства ?
        public string FurnitureMadeOf { get; set; }  // из чего сделано ?
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public string Manafacturer { get; set; }
        public bool AvailableProduct { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public int CategoryId { get; set; }
    }
}
