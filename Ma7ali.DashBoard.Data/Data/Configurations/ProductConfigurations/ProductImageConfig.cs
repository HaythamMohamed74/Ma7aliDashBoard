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
    public class ProductImageConfig : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder
            .Property(p => p.Id)
            .UseIdentityColumn(1, 1);
        }
    }
}
