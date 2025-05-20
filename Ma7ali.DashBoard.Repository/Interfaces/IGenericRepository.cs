using Ma7ali.DashBoard.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Repository.Interfaces
{
  public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<ICollection<TEntity>> GetAllAsync();
        Task <TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task <bool> DeleteAsync(int id);
        Task<PagedResult<TEntity>> GetSortedFilteredPagedAsync<TKey>(
            Expression<Func<TEntity, TKey>> orderBy,
            Expression<Func<TEntity, bool>> searchPredicate = null,
            bool ascending = true,
            int pageNumber = 1,
            int pageSize = 10);

    }
}
