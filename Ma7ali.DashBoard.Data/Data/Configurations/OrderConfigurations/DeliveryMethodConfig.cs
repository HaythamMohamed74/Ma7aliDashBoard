using Ma7ali.DashBoard.Data.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Data.Configurations.OrderConfigurations
{
    public class DeliveryMethodConfig : IEntityTypeConfiguration<DeliveyMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveyMethod> builder)
        {
            builder.Property(D => D.Cost).HasColumnType("decimal(18,2)");
        }
    }
}
