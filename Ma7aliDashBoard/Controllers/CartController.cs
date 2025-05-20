using System.Threading.Tasks;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7ali.DashBoard.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ma7aliDashBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<CartDto>> GetCart()
        {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId))
                return Unauthorized();

            return Ok(await _cartService.GetCartAsync(buyerId));
        }

        [HttpPost("items")]
        public async Task<ActionResult<CartDto>> AddItemToCart(CartItemToAddDto item)
        {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId))
                return Unauthorized();

            return Ok(await _cartService.AddItemToCartAsync(buyerId, item));
        }

        [HttpPut("items")]
        public async Task<ActionResult<CartDto>> UpdateCartItem(CartItemToAddDto item)
        {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId))
                return Unauthorized();

            return Ok(await _cartService.UpdateCartItemAsync(buyerId, item));
        }

        [HttpDelete("items/{productId}")]
        public async Task<ActionResult<CartDto>> RemoveItemFromCart(int productId)
        {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId))
                return Unauthorized();

            return Ok(await _cartService.RemoveItemFromCartAsync(buyerId, productId));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCart()
        {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId))
                return Unauthorized();

            var result = await _cartService.DeleteCartAsync(buyerId);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}