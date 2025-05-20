using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Repository.Interfaces
{
  public interface IStoreRepository:IGenericRepository<Store>
    {
        public Task<List<Store>> StoreByName(string name);
    }
}
