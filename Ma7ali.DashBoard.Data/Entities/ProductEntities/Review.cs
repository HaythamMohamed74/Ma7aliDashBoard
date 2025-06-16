using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ma7ali.DashBoard.Data.Entities.UserEntities;
using Ma7ali.DashBoard.Data.Helper;
using Microsoft.EntityFrameworkCore;

namespace Ma7ali.DashBoard.Data.Entities.ProductEntities
{
    public class Review : BaseEntity
    {
        
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
