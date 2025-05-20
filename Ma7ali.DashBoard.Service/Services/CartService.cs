

using AutoMapper;
using Ma7ali.DashBoard.Data.Data.Contexts;
using Ma7ali.DashBoard.Data.Entities.CartEntities;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7ali.DashBoard.Service.Interfaces;
using Ma7aliDashBoard.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ma7ali.DashBoard.Service.Services
{
    public class CartService : ICartService
    {
        private readonly Ma7aliContext _context;
        private readonly IMapper _mapper;

        public CartService(Ma7aliContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<CartDto> GetCartAsync(string buyerId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.BuyerId == buyerId);

            if (cart == null)
            {
                cart = new Cart { BuyerId = buyerId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<CartDto>(cart);
        }


        public async Task<CartDto> AddItemToCartAsync(string buyerId, CartItemToAddDto item)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.BuyerId == buyerId);

            if (cart == null)
            {
                cart = new Cart { BuyerId = buyerId };
                _context.Carts.Add(cart);
            }

            var product = await _context.Products.Include(x=>x.Images).FirstOrDefaultAsync(p=>p.Id==item.ProductId);
            if (product == null)
                throw new Exception("Product not found");

            //Console.WriteLine($"Product {product.Id}: Has Images Collection: {product.Images != null}");
            //Console.WriteLine($"Product {product.Id}: Images Count: {product.Images?.Count() ?? 0}");

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (cartItem != null)
            {
                cartItem.Quantity += item.Quantity;
            }
            else
            {
                string imageUrl = product.Images != null && product.Images.Any()
            ? product.Images.First().ImageUrl
            : "";
                imageUrl = imageUrl.NormlizePath();
                cartItem = new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ImgUrl = imageUrl,
                    Price = product.Price,
                    Quantity = item.Quantity,
                    CartId = cart.Id
                };
                cart.Items.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> UpdateCartItemAsync(string buyerId, CartItemToAddDto item)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.BuyerId == buyerId);

            if (cart == null)
                throw new Exception("Cart not found");

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (cartItem == null)
                throw new Exception("Item not found in cart");

            cartItem.Quantity = item.Quantity;
            await _context.SaveChangesAsync();

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> RemoveItemFromCartAsync(string buyerId, int productId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.BuyerId == buyerId);

            if (cart == null)
                throw new Exception("Cart not found");

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem != null)
            {
                cart.Items.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> DeleteCartAsync(string buyerId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.BuyerId == buyerId);

            if (cart == null)
                return false;

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}