using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using System.ComponentModel.DataAnnotations;

namespace Ma7aliDashBoard.Api.Dtos
{
    public class ProductCreationDto
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        //public string ImgUrl { get; set; }
        public ICollection<ProductImageDto> Images { get; set; } = new HashSet<ProductImageDto>();

        public int Stock { get; set; }

        public int BarndId { get; set; }


        public int CategoryId { get; set; }

        //public ProductColor AvailableColor { get; set; }

        public int StoreId { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}

