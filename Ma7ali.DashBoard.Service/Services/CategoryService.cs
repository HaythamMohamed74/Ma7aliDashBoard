using AutoMapper;
using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using Ma7ali.DashBoard.Repository.Interfaces;
using Ma7ali.DashBoard.Repository.Repositories;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7ali.DashBoard.Service.Interfaces;
using Ma7aliDashBoard.Service.Dtos;
using Ma7aliDashBoard.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Service.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository,IMapper mapper) 
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<CategoryToReturnDto>> GetAllCategory()
        {
            var categories= await _categoryRepository.GetAllAsync();
            var mappedCategories=_mapper.Map<ICollection<CategoryToReturnDto>>(categories);
            return mappedCategories;
        }

        public async Task<CategoryToReturnDto> GetCategoryById(int id)
        {
            if (id <= 0)
                throw new ApiException("Invalid Category ID.");
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                throw new ApiException("Category Not Found");
            }
            var mappedCategory = _mapper.Map<CategoryToReturnDto>(category);
          
            return mappedCategory;
        }
        public async Task<CategoryToReturnDto> CreateCategory(CategoryToCreateDto categoryToCreateDto)
        {
            if (categoryToCreateDto == null)
                throw new ApiException(" Invalid Category Data ");

            var category = _mapper.Map<Category>(categoryToCreateDto);
            if (categoryToCreateDto.ImgUrl != null && categoryToCreateDto.ImgUrl.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(categoryToCreateDto.ImgUrl.FileName);
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/categories");

                // لو المجلد مش موجود أنشئه
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fullPath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await categoryToCreateDto.ImgUrl.CopyToAsync(stream);
                }

                category.ImgUrl = $"/images/categories/{fileName}";
            }
            var createdCategory = await _categoryRepository.AddAsync(category);
            var mappedCategory = _mapper.Map<CategoryToReturnDto>(createdCategory);
            return mappedCategory;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var store = await _categoryRepository.GetByIdAsync(id);
            if (store == null)
                throw new ApiException($"Store with ID {id} not found");

            //  حذف الصورة من wwwroot
            if (!string.IsNullOrWhiteSpace(store.ImgUrl))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", store.ImgUrl.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            var deleted = await _categoryRepository.DeleteAsync(id);

            if (!deleted)
                throw new ApiException($"Category with ID {id} not found.");

            return true;

        }

        public async Task<CategoryToReturnDto> EditCategory(CategoryToEditDto categoryDto)
        {
           if(categoryDto ==null || categoryDto.Id<=0)
                throw new ApiException("Invalid Cateory ");
           var existingCategory=await _categoryRepository.GetByIdAsync(categoryDto.Id);
            if (existingCategory == null)
                throw new ApiException("Category Not Found");
            existingCategory.Name = categoryDto.Name;
            existingCategory.ImgUrl = categoryDto.ImgUrl;
            
             await _categoryRepository.UpdateAsync(existingCategory);
            var mappedCategory = _mapper.Map<CategoryToReturnDto>(existingCategory);
            return mappedCategory;




        }
    }
}
