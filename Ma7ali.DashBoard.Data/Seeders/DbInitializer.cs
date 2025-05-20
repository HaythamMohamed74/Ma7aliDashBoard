using System;
using System.Linq;
using System.Threading.Tasks;
using Ma7ali.DashBoard.Data.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Ma7ali.DashBoard.Data.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ma7ali.DashBoard.Data.Seeders
{
    public static class DbInitializer
    {
        public static async Task Initialize(Ma7aliContext context, IServiceProvider serviceProvider)
        {
            try
            {
                // Get required services
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Create roles if they don't exist
                string[] roleNames = { "Admin", "User" };
                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                // Create admin user if it doesn't exist
                var adminEmail = "admin@ma7ali.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    adminUser = new User
                    {
                        UserName = "admin",
                        Email = adminEmail,
                        EmailConfirmed = true,
                        CreatedAt = DateTime.UtcNow,
                        ProfileImageUrl= "default-profile.png"

                    };

                    var result = await userManager.CreateAsync(adminUser, "Admin123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while seeding the database.", ex);
            }
        }
    }
} 