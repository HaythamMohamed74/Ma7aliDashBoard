//using Ma7ali.DashBoard.Data.Entities.Identity;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Ma7ali.DashBoard.Data.Helper
//{
//    public static class IdentitySeedingContext
//    {
//        public static async Task AddUserSeedd(UserManager<User> userManager)
//        {
//            Console.WriteLine("Seeding started...");

//            if (!userManager.Users.Any())
//            {
//                Console.WriteLine("No users found. Creating a new user...");

//                var user = new User()
//                {
//                    FirstName = "Haytham",
//                    LastName = "Mohamed",
//                    UserName = "HaythamMoahmedE",
//                    Email = "haytham.mohamed056@gmail.com",
//                    Country = "Egypt",
//                    Address = "Tanta"
//                };

//                var result = await userManager.CreateAsync(user, "Admin@123");

//                if (result.Succeeded)
//                {
//                    Console.WriteLine("User created successfully. Assigning role...");
//                    //await userManager.AddToRoleAsync(user, "Owner");
//                    //Console.WriteLine("Role assigned successfully.");
//                }
//                else
//                {
//                    Console.WriteLine("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
//                }
//            }
//            else
//            {
//                Console.WriteLine("Users already exist. Skipping seeding.");
//            }
//        }

//    }
//}
