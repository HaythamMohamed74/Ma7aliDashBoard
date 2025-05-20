//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using Ma7ali.DashBoard.Data;
//using Ma7ali.DashBoard.Data.Entities.ProductEntities;
//using Ma7ali.DashBoard.Service.Dtos;
//using Ma7ali.DashBoard.Service.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace Ma7ali.DashBoard.Service.Services
//{
//    public class ReviewService : IReviewService
//    {
//        private readonly Ma7aliContext _context;
//        private readonly IMapper _mapper;

//        public ReviewService(Ma7aliContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<ReviewDto> CreateReviewAsync(string userId, CreateReviewDto reviewDto)
//        {
//            var user = await _context.Users.FindAsync(userId);
//            if (user == null)
//                throw new Exception("User not found");

//            var product = await _context.Products.FindAsync(reviewDto.ProductId);
//            if (product == null)
//                throw new Exception("Product not found");

//            var review = new Review
//            {
//                ProductId = reviewDto.ProductId,
//                UserId = userId,
//                UserName = user.UserName,
//                Rating = reviewDto.Rating,
//                Comment = reviewDto.Comment,
//                CreatedAt = DateTime.UtcNow
//            };

//            _context.Reviews.Add(review);
//            await _context.SaveChangesAsync();

//            return _mapper.Map<ReviewDto>(review);
//        }

//        public async Task<ReviewDto> UpdateReviewAsync(int reviewId, string userId, UpdateReviewDto reviewDto)
//        {
//            var review = await _context.Reviews.FindAsync(reviewId);
//            if (review == null)
//                throw new Exception("Review not found");

//            if (review.UserId != userId)
//                throw new Exception("You are not authorized to update this review");

//            review.Rating = reviewDto.Rating;
//            review.Comment = reviewDto.Comment;

//            await _context.SaveChangesAsync();

//            return _mapper.Map<ReviewDto>(review);
//        }

//        public async Task<bool> DeleteReviewAsync(int reviewId, string userId)
//        {
//            var review = await _context.Reviews.FindAsync(reviewId);
//            if (review == null)
//                throw new Exception("Review not found");

//            if (review.UserId != userId)
//                throw new Exception("You are not authorized to delete this review");

//            _context.Reviews.Remove(review);
//            await _context.SaveChangesAsync();

//            return true;
//        }

//        public async Task<ReviewDto> GetReviewByIdAsync(int reviewId)
//        {
//            var review = await _context.Reviews.FindAsync(reviewId);
//            if (review == null)
//                throw new Exception("Review not found");

//            return _mapper.Map<ReviewDto>(review);
//        }

//        public async Task<IEnumerable<ReviewDto>> GetProductReviewsAsync(int productId)
//        {
//            var reviews = await _context.Reviews
//                .Where(r => r.ProductId == productId)
//                .OrderByDescending(r => r.CreatedAt)
//                .ToListAsync();

//            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
//        }

//        public async Task<IEnumerable<ReviewDto>> GetUserReviewsAsync(string userId)
//        {
//            var reviews = await _context.Reviews
//                .Where(r => r.UserId == userId)
//                .OrderByDescending(r => r.CreatedAt)
//                .ToListAsync();

//            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
//        }
//    }
//} 