using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using Ma7aliDashBoard.Api.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Service.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public ICollection<ProductImageDto> Images { get; set; } = new HashSet<ProductImageDto>();

        public int Stock { get; set; }

        public int BarndId { get; set; }

        public string BrandName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        //public ProductColor AvailableColor { get; set; }

        public int? StoreId { get; set; }
        public DateTime CreationTime { get; set; }=DateTime.Now;
    }
}
