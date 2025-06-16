using Ma7ali.DashBoard.Data.Data.Contexts;
using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7ali.DashBoard.Data.Helper;
using Ma7ali.DashBoard.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
       // private readonly DbSet<Product> products;
        private readonly Ma7aliContext _ma7AliContext;

        public ProductRepository(Ma7aliContext ma7AliContext) : base(ma7AliContext)
        {

          //  products = _ma7AliContext.Set<Product>();
             _ma7AliContext = ma7AliContext;
        }
       
        public async Task<ICollection<Product>> GetAllProductAsync(Expression<Func<Product, bool>> expression = null)
        {
            if (expression == null)
            {
                return await _ma7AliContext.Set<Product>().Include(x => x.Images).Include(x => x.Category).ToListAsync();
            }
            else
            {
                return await _ma7AliContext.Set<Product>().Include(x => x.Images).Include(x => x.Category).Where(expression).ToListAsync();
            }
        }

        public async Task<ICollection<Product>> GetBestSallerProductsAsync()
        {
          var bestSeller=  await _ma7AliContext.OrderItems.GroupBy(x => x.ProductId).Select(x => new
            {
                ProductId = x.Key,
              TotalQuantity = x.Sum(x=>x.Quantity)
            }).OrderByDescending(x => x.TotalQuantity).Take(10).Select(x=>x.ProductId).ToListAsync();

            //var productIds = bestSellers.Select(x => x.ProductId).ToList();

            var products = await _ma7AliContext.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Where(p => bestSeller.Contains(p.Id))
                .ToListAsync();

            var orderedProducts = bestSeller
                .Select(id => products.First(p => p.Id == id))
                .ToList();


            return orderedProducts;


        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _ma7AliContext.Set<Product>().Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<Product>> GetTopRatedProductsAsync()
        {
            var topRatedProducts = await _ma7AliContext.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Include(p=>p.reviews)
                  .Where(p => p.reviews.Any()) 
                  .OrderByDescending(p => p.reviews.Average(r => r.Rating))
                .Take(10)
                .ToListAsync();

            return topRatedProducts;

        }
    }
}
