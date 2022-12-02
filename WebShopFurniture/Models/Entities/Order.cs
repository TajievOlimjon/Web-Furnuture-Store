using System.ComponentModel.DataAnnotations;

namespace WebShopFurniture.Models.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }

    }
}
