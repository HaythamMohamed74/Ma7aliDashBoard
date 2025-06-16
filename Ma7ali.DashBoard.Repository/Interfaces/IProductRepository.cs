using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Repository.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public  Task<Product> GetProductByIdAsync(int id);
        public Task<ICollection<Product>> GetBestSallerProductsAsync();

        public Task<ICollection<Product>> GetTopRatedProductsAsync();
    }
}
