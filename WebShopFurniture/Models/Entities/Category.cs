using System.ComponentModel.DataAnnotations;

namespace WebShopFurniture.Models.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Введите название категории ?")]
        [Display(Name ="Названия категории"), MaxLength(65)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Введите название категории ?")]
        [Display(Name = "Кароткое описания"), MaxLength(100)]
        public string ShortDesc { get; set; }

        [Display(Name = "Польное описания")]
        public string FullDesc { get; set; }
        public virtual List<Product>? Products { get; set; }

    }
}
