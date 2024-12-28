using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Entities.ProductEntities
{
    public class ProductImage:BaseEntity
    {
        public string ImageUrl { get; set; }
        public  Product Product { get; set; }
        public int  ProductId { get; set; }

    }
}
