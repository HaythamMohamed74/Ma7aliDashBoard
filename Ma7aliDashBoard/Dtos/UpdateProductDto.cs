namespace Ma7aliDashBoard.Api.Dtos
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public ICollection<ProductImageDto> Images { get; set; } = new HashSet<ProductImageDto>();

        public int Stock { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
