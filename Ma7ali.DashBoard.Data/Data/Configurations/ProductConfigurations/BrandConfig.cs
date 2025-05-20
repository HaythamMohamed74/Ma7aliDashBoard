//using Ma7ali.DashBoard.Data.Entities.ProductEntities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Ma7ali.DashBoard.Data.Data.Configurations.ProductConfigurations
//{
//    public class BrandConfig : IEntityTypeConfiguration<Brand>
//    {
//        public void Configure(EntityTypeBuilder<Brand> builder)
//        {
//            builder.HasMany(b=>b.Products).WithOne(p=>p.Brand);
//            builder.Property(b=>b.Name).IsRequired().HasMaxLength(100);
//            builder.HasKey(p => p.Id);
//        }
//    }
//}
