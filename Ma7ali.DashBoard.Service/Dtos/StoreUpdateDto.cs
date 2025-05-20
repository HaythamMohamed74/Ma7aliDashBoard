using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Service.Dtos
{
   public class StoreUpdateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Store name is required.")]
        [StringLength(100, ErrorMessage = "Store name must be less than 100 characters.")]
        public string StoreName { get; set; }

        [Required(ErrorMessage = "Store Desc is required.")]
        [StringLength(300, ErrorMessage = "Store Desc must be less than 300 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Store Image is required.")]
        //[(300, ErrorMessage = "Store Desc must be less than 300 characters.")]
        public string StoreImg { get; set; }
        public string? StoreBackGroundLogo { get; set; }

        public int StoreTempletes { get; set; } = 1;
    }
}
