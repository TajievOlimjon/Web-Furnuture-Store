using System.ComponentModel.DataAnnotations;

namespace WebShopFurniture.Models.Entities
{
    public class Cart // корзина 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string CartId { get; set; }  // нужен для сесси 
    }
}
