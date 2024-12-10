using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Data.Configurations.ProductConfigurations
{
    public class productConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            
            builder.HasOne(p=>p.Brand).WithMany(b=>b.Products).HasForeignKey(p=>p.BarndId);
            builder.HasOne(p=>p.Category).WithMany(c=>c.Products).HasForeignKey(p=>p.CategoryId);
            builder.HasKey(p=>p.Id);
            builder.Property(p => p.Id).UseIdentityColumn(1, 1);
        }
    }
}
