using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Entities.StoreEntities
{
    public class Store:BaseEntity
    {
        public  string StoreName { get; set; }
        public  string Description { get; set; }
        
        public string StoreImg { get; set; }
        public string StoreBackGroundLogo { get; set; }

        public int StoreTempletes { get; set; }

        public ICollection<Category> StoreCategories { get; set; }= new HashSet<Category>();
        public ICollection<Product> StoreProducts { get; set; }= new HashSet<Product>();

      






    }
}
