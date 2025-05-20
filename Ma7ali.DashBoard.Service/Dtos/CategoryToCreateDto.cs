using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Service.Dtos
{
   public class CategoryToCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Image  is required")]
     
        public IFormFile ImgUrl { get; set; }

        public int?  StoreId { get; set; }

    }
}
