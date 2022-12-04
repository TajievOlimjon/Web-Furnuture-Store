using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebShopFurniture.Models.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите названия продукта")]
        [Display(Name ="названия"),MaxLength(85)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Введите название категории ?")]
        [Display(Name = "Кароткое описания"), MaxLength(100)]
        public string ShortDesc { get; set; }

        [Display(Name = "Польное описания")]
        public string FullDesc { get; set; }
        public DateTimeOffset date { get; set; }  // дата производства ?
        public string FurnitureMadeOf { get; set; }  // из чего сделано ?

        [Required(ErrorMessage ="Введите цену")]
        [Display(Name ="Цена")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Введите количество продукта")]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }
        [Display(Name ="Производитель")]
        public string Manafacturer { get; set; }
        [Display(Name ="Продукт доступен для покупки или нет ?")]
        public bool AvailableProduct { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        [Display(Name ="Фото продукта")]
        [Required(ErrorMessage = "Добавте фото для продукта")]
        public string Image { get; set; }

        [Required(ErrorMessage ="Введите категорию товара")]
        [Display(Name ="Категория")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
