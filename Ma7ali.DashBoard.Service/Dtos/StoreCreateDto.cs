
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ma7aliDashBoard.Service.Dtos
{
    public class StoreCreateDto
    {
        
        [Required(ErrorMessage = "Store name is required.")]
        [StringLength(100, ErrorMessage = "Store name must be less than 100 characters.")]
        public string StoreName { get; set; }

        [Required(ErrorMessage = "Store Desc is required.")]
        [StringLength(300, ErrorMessage = "Store Desc must be less than 300 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Store Image is required.")]
        //[(300, ErrorMessage = "Store Desc must be less than 300 characters.")]
        
        public IFormFile StoreImage { get; set; }
        public string? StoreBackGroundLogo { get; set; }

        public int StoreTempletes { get; set; } = 1;


    }
}
