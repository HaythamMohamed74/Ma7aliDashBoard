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
            builder.HasKey(p => p.Id);
            //builder.Property(p => p.BarndId)
            //    .IsRequired(false);
            builder
                .Property(p=>p.Price)
                .HasColumnType("decimal(18,2)");
            builder.
                 Property(p => p.Name)
                .IsRequired();
            //builder.HasOne(p=>p.Brand)
            //    .WithMany(b=>b.Products)
            //    .HasForeignKey(p=>p.BarndId).OnDelete(DeleteBehavior.SetNull);
            builder
                .HasOne(p=>p.Category)
                .WithMany(c=>c.Products)
                .HasForeignKey(p=>p.CategoryId);
            builder
                .HasMany(p => p.Images)
                .WithOne(x=>x.Product)
                .HasForeignKey(i=>i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(p => p.Id)
                .UseIdentityColumn(1, 1);
            //builder.Property(p => p.AvailableSize).HasConversion(ps=>ps.ToString(),ps=>(ProductSize)Enum.Parse(typeof(ProductSize),ps));
            //builder.Property(p => p.AvailableColor)
            //.HasConversion(
            //c => c.ToString(),
            //   v => (ProductColor)Enum.Parse(typeof(ProductColor), v));
            //    builder.Property(p=>p.AvailableColor)
            //.HasConversion<string>()
            //.HasMaxLength(50);
            builder.HasOne(p=>p.Store).WithMany(s=>s.StoreProducts).HasForeignKey(p=>p.StoreId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
