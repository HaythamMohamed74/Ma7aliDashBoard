using System.Threading.Tasks;
using Ma7ali.DashBoard.Service.Dtos;

namespace Ma7ali.DashBoard.Service.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(string buyerId);
        Task<CartDto> AddItemToCartAsync(string buyerId, CartItemToAddDto item);
        Task<CartDto> UpdateCartItemAsync(string buyerId, CartItemToAddDto item);
        Task<CartDto> RemoveItemFromCartAsync(string buyerId, int productId);
        Task<bool> DeleteCartAsync(string buyerId);
    }
}