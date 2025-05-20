using AutoMapper;
using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using Ma7ali.DashBoard.Data.Helper;
using Ma7ali.DashBoard.Repository.Interfaces;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7ali.DashBoard.Service.Interfaces;
using Ma7aliDashBoard.Service.Dtos;
using Ma7aliDashBoard.Shared.Exceptions;
using Ma7aliDashBoard.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Service.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public StoreService(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public async Task<StoreDto> CreateStore(StoreCreateDto storeDto)
        {
            if (storeDto == null)
                throw new ApiException(" Invalid Store Data ");
            var store = _mapper.Map<Store>(storeDto);
            if (storeDto.StoreImage != null && storeDto.StoreImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(storeDto.StoreImage.FileName);
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/stores");

                // لو المجلد مش موجود أنشئه
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fullPath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await storeDto.StoreImage.CopyToAsync(stream);
                }

                store.StoreImg = $"/images/stores/{fileName}";
            }

            var createdStore = await _storeRepository.AddAsync(store);
            var mappedStore = _mapper.Map<StoreDto>(createdStore);

            return mappedStore;


        }

        public async Task<bool> DeleteStore(int id)
        {
            var store = await _storeRepository.GetByIdAsync(id);
            if (store == null)
                throw new ApiException($"Store with ID {id} not found");

            //  حذف الصورة من wwwroot
            if (!string.IsNullOrWhiteSpace(store.StoreImg))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", store.StoreImg.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            var deleted = await _storeRepository.DeleteAsync(id);
            if (!deleted)
                throw new ApiException($"Failed to delete store with ID {id}");


            return true;



        }

        public async Task<StoreDto> EditStore(StoreUpdateDto storeDto)
        {


            var existingStore = await _storeRepository.GetByIdAsync(storeDto.Id);
            if (existingStore is null)
                throw new ApiException($"Store with {storeDto.Id} not Found");

            existingStore.StoreName = storeDto.StoreName;
            existingStore.StoreBackGroundLogo = storeDto.StoreBackGroundLogo!;
            existingStore.Description = storeDto.Description;

            await _storeRepository.UpdateAsync(existingStore);
            var mappedStore = _mapper.Map<StoreDto>(existingStore);
            return mappedStore;

        }

        public async Task<ICollection<StoreDto>> GetAllStores()
        {

            var stores = await _storeRepository.GetAllAsync();
            var mappedStores = _mapper.Map<ICollection<StoreDto>>(stores);
            return mappedStores;
        }

        public async Task<StoreDto> GetStoreById(int id)
        {
            if (id <= 0)

                throw new ApiException($"Ivalid Store with {id}");
            var store = await _storeRepository.GetByIdAsync(id);
            var mappedStore = _mapper.Map<StoreDto>(store);
            return mappedStore;
        }

        public async Task<List<StoreDto>> GetStoreByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ApiException($"name is Required");
            var store= await  _storeRepository.StoreByName(name);
            if (store == null)
                throw new ApiException($"No store found with name: {name}");
            var mappedStore = _mapper.Map<List<StoreDto>>(store);
            return mappedStore;
        }

        public async Task<PagedResult<StoreDto>> GetStoresPagedAsync(PagedRequestDto request)
        {
            Expression<Func<Store, bool>> searchPredicate = null;
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                searchPredicate = s => s.StoreName.Contains(request.Search) || s.Description.Contains(request.Search);
            }

            Expression<Func<Store, object>> orderBy = request.SortBy switch
            {
                "StoreName" => s => s.StoreName,
                "Description" => s => s.Description,
                _ => s => s.Id
            };

            var pagedResult = await _storeRepository.GetSortedFilteredPagedAsync(orderBy, searchPredicate!, request.Ascending, request.PageNumber, request.PageSize);

            return new PagedResult<StoreDto>
            {
                Items = _mapper.Map<List<StoreDto>>(pagedResult.Items),
                TotalCount = pagedResult.TotalCount,
                PageNumber = pagedResult.PageNumber > 0 ? pagedResult.PageNumber : 1,
                PageSize = pagedResult.PageSize > 0 ? pagedResult.PageSize : 10,
            };
        }
    }
}
