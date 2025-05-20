using Ma7ali.DashBoard.Data.Data.Contexts;
using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using Ma7ali.DashBoard.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Repository.Repositories
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {

        private readonly DbSet<Store>_stores;
        public StoreRepository(Ma7aliContext ma7AliContext) : base(ma7AliContext)
        {
            _stores = ma7AliContext.Set<Store>();
        }
        public new async Task<ICollection<Store>> GetAllAsync()
        {
            return await _stores.Include(x=>x.StoreProducts).ThenInclude(x=>x.Images).Include(x => x.StoreCategories).ToListAsync();
        }
        public new  async Task<Store> GetByIdAsync(int id)
        {

            return await  _stores.
                Include(x=>x.StoreProducts).ThenInclude(x => x.Images)
                .Include(x => x.StoreCategories)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Store>> StoreByName(string name)
        {
            var result=  await _stores.Where(x=>x.StoreName.ToLower()
             .Contains(name.ToLower())).Include(x=>x.StoreProducts).Include(x=>x.StoreCategories)
                .ToListAsync();
            return result;

           
        }
    }
}
