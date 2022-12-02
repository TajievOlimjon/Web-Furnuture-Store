using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebShopFurniture.Models.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Имя"), Required(ErrorMessage = "Пожолуйста ведите  имя? ")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия"), Required(ErrorMessage = "Пожолуйста ведите фамилию ?")]
        public string LastName { get; set; }
        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Display(Name = " Номер телефона"), Required(ErrorMessage = "Пожолуйста ведите номер телефона? ")]
        public string PhoneNumber { get; set; }

        [Display(Name = " Электронная почта")]
        [Required(ErrorMessage = "Пожолуйста ведите электронную почту ?")]
        [EmailAddress(ErrorMessage ="Некороектное электронная почта ")]
        public string Email { get; set; }
        public DateTimeOffset OrderTime { get; set; }
        public virtual List<Order>? Orders { get; set; }
    }

}
