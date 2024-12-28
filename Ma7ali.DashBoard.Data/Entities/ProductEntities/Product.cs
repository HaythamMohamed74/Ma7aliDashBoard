using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Entities.ProductEntities
{
    public class Product : BaseEntity
    {
       
        public string Name { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        //[DataType(DataType.ImageUrl)]   
                                        
        public int Stock { get; set; }

        public Brand Brand { get; set; }
        public int BarndId { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public ProductColor AvailableColor { get; set; }

        public int? StoreId { get; set; }
        public Store? Store { get; set; }

        public ICollection<ProductImage> Images { get; set; } = new HashSet<ProductImage>();

    }
}
