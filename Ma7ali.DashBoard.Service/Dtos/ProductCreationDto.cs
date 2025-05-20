using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ma7aliDashBoard.Service.Dtos
{
    public class ProductCreationDto
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        
        public IFormFileCollection Images { get; set; }

        public int Stock { get; set; }

        public int BarndId { get; set; }


        public int CategoryId { get; set; }


        public int StoreId { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}

