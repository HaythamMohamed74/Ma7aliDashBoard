using System;
using Microsoft.AspNetCore.Identity;

namespace Ma7ali.DashBoard.Data.Entities.UserEntities
{
    public class User : IdentityUser
    {
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
    }
} 