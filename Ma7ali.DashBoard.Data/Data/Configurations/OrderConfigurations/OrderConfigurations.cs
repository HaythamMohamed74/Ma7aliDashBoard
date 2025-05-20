//using Ma7ali.DashBoard.Data.Entities.OrderEntities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Ma7ali.DashBoard.Data.Data.Configurations.OrderConfigurations
//{
//    public class OrderConfigurations : IEntityTypeConfiguration<Order>
//    {
//        public void Configure(EntityTypeBuilder<Order> builder)

//        {
//            builder.Property(o => o.OrderStatus).HasConversion(Os => Os.ToString(), Os => (OrderStatus)Enum.Parse(typeof(OrderStatus), Os));
//            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
//            builder.OwnsOne(o => o.OrderAddress, ad => ad.WithOwner());
//            builder.HasOne(o=>o.DeliveyMethod).WithMany().OnDelete(DeleteBehavior.NoAction);
//        }
//    }
//}
