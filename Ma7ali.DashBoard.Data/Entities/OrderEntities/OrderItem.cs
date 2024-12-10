using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Entities.OrderEntities
{
    public class OrderItem:BaseEntity
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImgUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
