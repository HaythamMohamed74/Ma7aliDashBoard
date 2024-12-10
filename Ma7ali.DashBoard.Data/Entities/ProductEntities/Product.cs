using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Entities.ProductEntities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImgUrl { get; set; }

        public int Stock { get; set; }

        public Brand Brand { get; set; }
        public int BarndId { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }




    }
}
