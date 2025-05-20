using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using Ma7ali.DashBoard.Data.Helper;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7aliDashBoard.Service.Dtos;
using Ma7aliDashBoard.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Service.Interfaces
{
    public interface IStoreService
    {

        public Task<ICollection<StoreDto>> GetAllStores();
        public Task<StoreDto> GetStoreById(int id);

        public Task<StoreDto> CreateStore(StoreCreateDto storeDto);

        public Task<bool> DeleteStore(int id);

        public Task<StoreDto> EditStore(StoreUpdateDto storeDto);

        public Task<List<StoreDto>>GetStoreByName(string name );

        Task<PagedResult<StoreDto>> GetStoresPagedAsync(PagedRequestDto request);



    }



}

