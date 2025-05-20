using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7ali.DashBoard.Data.Helper;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7aliDashBoard.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Service.Interfaces
{
   public interface IProductService
    {
        Task<ICollection<ProductDto>> GetAllProduct();

        Task<ProductDto> GetProductById(int id);

        Task<ProductDto> AddProduct(ProductCreationDto productCreationDto);

        Task<ProductDto> UpdateProduct(ProductToUpdateDto productToUpdateDto);

        Task<bool> DeleteProduct(int id);

        Task<PagedResult<ProductDto>> GetSortedFilteredPagedAsync(string search ,int page, int size);

        Task <ICollection<ProductDto>>GetBestSallerProducts();
    }
}
