using Ma7ali.DashBoard.Data.Data.Contexts;
using Ma7ali.DashBoard.Data.Helper;
using Ma7ali.DashBoard.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Repository.Repositories
{
  public class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity : class
    {
        private readonly Ma7aliContext _ma7AliContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(Ma7aliContext ma7AliContext)
        {
            _ma7AliContext = ma7AliContext;
            _dbSet = _ma7AliContext.Set<TEntity>();
        }
        public async Task<TEntity>AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _ma7AliContext.SaveChangesAsync();
            return entity;
        }
        public  async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _ma7AliContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
           
            return await _dbSet.FindAsync(id);
        }

        public async Task<PagedResult<TEntity>> GetSortedFilteredPagedAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, Expression<Func<TEntity, bool>> searchPredicate = null, bool ascending = true, int pageNumber = 1, int pageSize = 10)
        {

            IQueryable<TEntity> query = _dbSet;

            if (searchPredicate != null)
            {
                query = query.Where(searchPredicate);
            }
            int totalCount = await query.CountAsync();

            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            query=query.Skip((pageNumber-1)*pageSize).Take(pageSize);
            var items = await query.ToListAsync();

            return new PagedResult<TEntity>
            {  
               Items= items, 
               TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber
              
            };
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _ma7AliContext.SaveChangesAsync();
        }
    }
    


    
}
