using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ma7ali.DashBoard.Data.Data.Contexts;
using Ma7ali.DashBoard.Data.Entities.CartEntities;
using Ma7ali.DashBoard.Data.Entities.OrderEntities;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7ali.DashBoard.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ma7ali.DashBoard.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly Ma7aliContext _context;
        private readonly IMapper _mapper;

        public OrderService(Ma7aliContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateOrderAsync(string buyerId, OrderAddressDto address, int deliveryMethodId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.BuyerId == buyerId);

            if (cart == null)
                throw new Exception("Cart not found");

            var deliveryMethod = await _context.DeliveyMethods.FindAsync(deliveryMethodId);
            if (deliveryMethod == null)
                throw new Exception("Delivery method not found");

            var orderItems = new List<OrderItem>();
            decimal subtotal = 0;

            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ImgUrl = item.ImgUrl,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                orderItems.Add(orderItem);
                subtotal += item.Price * item.Quantity;
            }

            var order = new Order
            {
                BuyerEmail = buyerId,
                OrderAddress = _mapper.Map<OrderAddress>(address),
                DeliveyMethod = deliveryMethod,
                OrderItems = orderItems,
                SubTotal = subtotal
            };

            _context.Orders.Add(order);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id, string buyerId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.DeliveyMethod)
                .FirstOrDefaultAsync(o => o.Id == id && o.BuyerEmail == buyerId);

            if (order == null)
                throw new Exception("Order not found");

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersForUserAsync(string buyerId)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.DeliveyMethod)
                .Where(o => o.BuyerEmail == buyerId)
                .OrderByDescending(o => o.OrderTime)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> UpdateOrderStatusAsync(int id, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                throw new Exception("Order not found");

            order.OrderStatus = status;
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }
    }
}