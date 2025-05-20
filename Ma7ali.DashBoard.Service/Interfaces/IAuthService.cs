using System.Threading.Tasks;
using Ma7ali.DashBoard.Service.Dtos;

namespace Ma7ali.DashBoard.Service.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<UserProfileDto> GetUserProfileAsync(int userId);
        Task<UserProfileDto> UpdateProfileAsync(int userId, UpdateProfileDto updateDto);
        Task<bool> UpdateProfileImageAsync(int userId, string imageUrl);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    }
} 