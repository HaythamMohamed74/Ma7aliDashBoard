using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Data.Configurations.ProductConfigurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(b => b.Products).WithOne(p => p.Category);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(100);
            builder.HasKey(p => p.Id);
        }
    }
}
