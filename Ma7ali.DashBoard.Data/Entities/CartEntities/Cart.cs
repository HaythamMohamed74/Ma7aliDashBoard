using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Entities.CartEntities
{
    public class Cart:BaseEntity
    {
        
            public string BuyerId { get; set; }
            public ICollection<CartItem> Items { get; set; } = new HashSet<CartItem>();
        
    }
}
