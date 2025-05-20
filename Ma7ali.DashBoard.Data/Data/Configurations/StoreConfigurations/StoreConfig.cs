using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Data.Configurations.StoreConfigurations
{
    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Store> builder)
        {
            builder.HasMany(s => s.StoreProducts).WithOne(p => p.Store).HasForeignKey(p => p.StoreId);
            builder.HasMany(s => s.StoreCategories).WithOne(p => p.Store).HasForeignKey(p => p.StoreId);
            builder.Property(s => s.StoreName).IsRequired();
            builder.HasIndex(s=>s.StoreName);
        }
    }
}
