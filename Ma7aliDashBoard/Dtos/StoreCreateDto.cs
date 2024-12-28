using System.ComponentModel.DataAnnotations;

namespace Ma7aliDashBoard.Api.Dtos
{
    public class StoreCreateDto
    {
        [Required(ErrorMessage = "Store name is required.")]
        [StringLength(100, ErrorMessage = "Store name must be less than 100 characters.")]
        public string StoreName { get; set; }
        public string Description { get; set; }
        public string StoreImg { get; set; }
        public string? StoreBackGroundLogo { get; set; }

        public int StoreTempletes { get; set; } = 1;


    }
}
