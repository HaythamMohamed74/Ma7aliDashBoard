using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Entities.OrderEntities
{
    public class Order : BaseEntity
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderTime { get; set; }=DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public OrderAddress OrderAddress { get; set; }
        public DeliveyMethod DeliveyMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }=new HashSet<OrderItem>();

        public decimal SubTotal { get; set; }
    
        public decimal GetTotal()
        {
            return SubTotal+DeliveyMethod.Cost;
        }
    }
}
