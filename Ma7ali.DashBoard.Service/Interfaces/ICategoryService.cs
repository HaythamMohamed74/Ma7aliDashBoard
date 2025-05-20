using Ma7ali.DashBoard.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Service.Interfaces
{
   public interface ICategoryService
    {
        public Task<ICollection<CategoryToReturnDto>> GetAllCategory();
        public Task<CategoryToReturnDto> GetCategoryById(int id);
        public Task<CategoryToReturnDto> CreateCategory(CategoryToCreateDto categoryToCreateDto);
        public Task<bool> DeleteCategory(int id);

        public Task<CategoryToReturnDto> EditCategory(CategoryToEditDto categoryDto);


    }
}
