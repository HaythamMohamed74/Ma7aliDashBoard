

using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7aliDashBoard.Service.Dtos;
using Microsoft.AspNetCore.Http;

namespace Ma7ali.DashBoard.Service.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public List<string> Images { get; set; }

        public int Stock { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public double AvgRateing { get; set; }

        public int? StoreId { get; set; }
        public DateTime CreationTime { get; set; }=DateTime.Now;

        public DateTime LastUpdateTime { get; set; } = DateTime.Now;
    }
}
