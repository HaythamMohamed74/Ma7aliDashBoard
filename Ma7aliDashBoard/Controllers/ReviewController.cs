using Ma7ali.DashBoard.Service.Dtos;
using Ma7ali.DashBoard.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ma7aliDashBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // Create a new review
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReviewDto>> CreateReview([FromBody] CreateReviewDto reviewDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var createdReview = await _reviewService.CreateReviewAsync(userId, reviewDto);
            return CreatedAtAction(nameof(GetReviewById), new { reviewId = createdReview.Id }, createdReview);
        }

        // Update an existing review
        [HttpPut("{reviewId}")]
        [Authorize]
        public async Task<ActionResult<ReviewDto>> UpdateReview(int reviewId, [FromBody] UpdateReviewDto reviewDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var updatedReview = await _reviewService.UpdateReviewAsync(reviewId, userId, reviewDto);
            return Ok(updatedReview);
        }

        // Delete a review
        [HttpDelete("{reviewId}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var result = await _reviewService.DeleteReviewAsync(reviewId, userId);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // Get review by ID
        [HttpGet("{reviewId}")]
        public async Task<ActionResult<ReviewDto>> GetReviewById(int reviewId)
        {
            var review = await _reviewService.GetReviewByIdAsync(reviewId);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        // Get reviews of a specific product
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetProductReviews(int productId)
        {
            var reviews = await _reviewService.GetProductReviewsAsync(productId);
            return Ok(reviews);
        }

        // Get reviews made by the current user
        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetUserReviews()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var reviews = await _reviewService.GetUserReviewsAsync(userId);
            return Ok(reviews);
        }
    }
}
