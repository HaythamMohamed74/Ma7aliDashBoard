using System.Collections.Generic;
using System.Threading.Tasks;
using Ma7ali.DashBoard.Service.Dtos;

namespace Ma7ali.DashBoard.Service.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewDto> CreateReviewAsync(string userId, CreateReviewDto reviewDto);
        Task<ReviewDto> UpdateReviewAsync(int reviewId, string userId, UpdateReviewDto reviewDto);
        Task<bool> DeleteReviewAsync(int reviewId, string userId);
        Task<ReviewDto> GetReviewByIdAsync(int reviewId);
        Task<IEnumerable<ReviewDto>> GetProductReviewsAsync(int productId);
        Task<IEnumerable<ReviewDto>> GetUserReviewsAsync(string userId);
    }
}