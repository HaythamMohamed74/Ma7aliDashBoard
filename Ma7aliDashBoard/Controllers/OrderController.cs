using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ma7ali.DashBoard.Data.Entities.OrderEntities;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7ali.DashBoard.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ma7aliDashBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderAddressDto address, int deliveryMethodId)
        {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId))
                return Unauthorized();

            return Ok(await _orderService.CreateOrderAsync(buyerId, address, deliveryMethodId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId))
                return Unauthorized();

            return Ok(await _orderService.GetOrderByIdAsync(id, buyerId));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersForUser()
        {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId))
                return Unauthorized();

            return Ok(await _orderService.GetOrdersForUserAsync(buyerId));
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<OrderDto>> UpdateOrderStatus(int id, OrderStatus status)
        {
            return Ok(await _orderService.UpdateOrderStatusAsync(id, status));
        }
    }
}