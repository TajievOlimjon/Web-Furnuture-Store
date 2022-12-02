using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebShopFurniture.Models.Entities;

namespace WebShopFurniture.ConfigurationEntities
{
    public class ConfigurationCategory:IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            var categories = new List<Category>()
            {
                new Category
                {
                       
                },
                new Category
                {
                    
                }
            };
            builder.HasData(categories);
        }
    }
}
