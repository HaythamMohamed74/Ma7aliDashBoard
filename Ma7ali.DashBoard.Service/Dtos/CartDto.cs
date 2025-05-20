using System;
using System.Collections.Generic;

namespace Ma7ali.DashBoard.Service.Dtos
{
    public class CartDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<CartItemDto> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImgUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CartItemToAddDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
} 