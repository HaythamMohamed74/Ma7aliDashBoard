using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Entities.ProductEntities
{
    public class Review:BaseEntity
    {

        public  int  UserId { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Range(1, 5, ErrorMessage = "Review rate must be between 1 and 5.")]
        public  double ReviewRate { get; set; }


        public string? Comment { get; set; }




    }
}
