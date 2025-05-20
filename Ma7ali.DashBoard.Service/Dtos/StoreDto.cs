using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7ali.DashBoard.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7aliDashBoard.Service.Dtos
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string? Description { get; set; }

        public string? StoreImg { get; set; }
        public string? StoreBackGroundLogo { get; set; }



        public int StoreTempletes { get; set; }

        public ICollection<CategoryToReturnDto> StoreCategories { get; set; } = new HashSet<CategoryToReturnDto>();
        public ICollection<ProductDto> StoreProducts { get; set; } = new HashSet<ProductDto>();

        public int ProductCount { get; set; }



    }
}
