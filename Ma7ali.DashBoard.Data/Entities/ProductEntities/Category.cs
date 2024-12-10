using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Entities.ProductEntities
{
    public class Category : BaseEntity
    {
        
        public string Name { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImgUrl { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
