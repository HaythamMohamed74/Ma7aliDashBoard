using System.Threading.Tasks;
using Ma7ali.DashBoard.Data.Entities.OrderEntities;
using Ma7ali.DashBoard.Service.Dtos;

namespace Ma7ali.DashBoard.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(string buyerId, OrderAddressDto address, int deliveryMethodId);
        Task<OrderDto> GetOrderByIdAsync(int id, string buyerId);
        Task<IEnumerable<OrderDto>> GetOrdersForUserAsync(string buyerId);
        Task<OrderDto> UpdateOrderStatusAsync(int id, OrderStatus status);
    }
}