using System;
using System.Collections.Generic;
using Ma7ali.DashBoard.Data.Entities.OrderEntities;

namespace Ma7ali.DashBoard.Service.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public OrderAddressDto OrderAddress { get; set; }
        public DeliveryMethodDto DeliveryMethod { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }

    public class OrderAddressDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

    public class DeliveryMethodDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }

    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImgUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
} 