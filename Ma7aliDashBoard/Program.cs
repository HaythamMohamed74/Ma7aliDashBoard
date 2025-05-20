using Ma7ali.DashBoard.Data.Data.Contexts;
using Ma7ali.DashBoard.Data.Helper;
using Ma7ali.DashBoard.Repository.Interfaces;
using Ma7ali.DashBoard.Repository.Repositories;
using Ma7ali.DashBoard.Service.Interfaces;
using Ma7ali.DashBoard.Service.Services;
using Ma7aliDashBoard.Service.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using AutoMapper;
using Ma7aliDashBoard.Api.Middlewares;
using Microsoft.Extensions.FileProviders;
using System.Text;
using Ma7ali.DashBoard.Data.Seeders;
using Ma7ali.DashBoard.Data.Entities.UserEntities;

namespace Ma7aliDashBoard
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region ServiceCollection
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IStoreRepository, StoreRepository>();
            builder.Services.AddScoped<IStoreService, StoreService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IAuthService, AuthService>();


            builder.Services.AddSingleton<IFileProvider>(
          new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            builder.Services.AddScoped<IImageService, ImageService>();

  

            #endregion


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #region DbContexts
            builder.Services.AddDbContext<Ma7aliContext>(options =>
              {
                  options.UseSqlServer(
                      builder.Configuration.GetConnectionString("DefaultConnection"));
                  //sqlOptions =>
                  //{
                  //    sqlOptions.EnableRetryOnFailure(
                  //        maxRetryCount: 5, // Number of retry attempts
                  //        maxRetryDelay: TimeSpan.FromSeconds(10), // Max delay between retries
                  //        errorNumbersToAdd: null // Additional SQL error codes to consider transient
                  //    );
                  //});
              });
            //builder.Services.AddDbContext<Ma7aliIdentityContext>(options =>
            //{
            //    options.UseSqlServer(
            //        builder.Configuration.GetConnectionString("IdentityConnection"));
            //});
           

            #endregion
            //#region ConfigJWT
            //builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            //var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

            //builder.Services.AddIdentity<User, Role>(options =>
            //{
            //    options.Password.RequiredLength = 8;
            //}).AddEntityFrameworkStores<Ma7aliContext>().AddDefaultTokenProviders();

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = jwtSettings.Issuer,
            //        ValidAudience = jwtSettings.Audience,
            //        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings.Key))
            //    };
            //});

            //#endregion
            #region Cors
            builder.Services.AddCors(options =>
           {
               options.AddDefaultPolicy(
                   builder =>
                   {
                       builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                   });
           }); 
            #endregion

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });
             builder.Services.AddAutoMapper(typeof(MappingProfile));
    
            #region Identity
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;

                // User settings
                options.User.RequireUniqueEmail = true;
            })
.AddEntityFrameworkStores<Ma7aliContext>()
.AddDefaultTokenProviders();
            #endregion
            builder.Services.AddLogging(logging =>
            {
                logging.AddConsole();  // For console logging
                  // For file-based logging
                                                  // Or use any other logging provider
            });
            #region AuthService
            builder.Services.AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
                .AddJwtBearer(options =>
                {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ClockSkew = TimeSpan.Zero
                   };
                }); 
            #endregion


            var app = builder.Build();
            #region DataBase -Update

            //using var scope = app.Services.CreateScope();
            //var services = scope.ServiceProvider;
            //var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            //try
            //{
            //    var context = services.GetRequiredService<Ma7aliContext>();
            //    await context.Database.MigrateAsync();
            //    //await SeedingContext.SeedingAsync(context, loggerFactory);

            //    //var contextIdentity = services.GetRequiredService<Ma7aliContext>();
            //    //await contextIdentity.Database.MigrateAsync();

            //    //var userManager = services.GetRequiredService<UserManager<User>>();
            //    //var logger = loggerFactory.CreateLogger<Program>();
            //    //logger.LogInformation("Seeding identity users...");
            //    //await IdentitySeedingContext.AddUserSeedd(userManager);
            //    //logger.LogInformation("Seeding completed successfully.");
            //}

            //catch (Exception e)
            //{
            //    var logs = loggerFactory.CreateLogger<Program>();
            //    logs.LogError("in program  " + e.Message.ToString());
            //}
            #endregion

            #region Seeding
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<Ma7aliContext>();
                    context.Database.Migrate();
                    await DbInitializer.Initialize(context,services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                    throw;
                }
            }

            #endregion


            //await DataBaseInitailizer.InitializeAsync(app.Services);

            // "defultConnection": "Data Source=DESKTOP-662AKIO\\SQLEXPRESS;Database=Ma7aliDashBoard;Integrated Security=True;Trust Server Certificate=True;MultipleActiveResultSets=true"
            ////Za5!_Ph97@Dz;
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()||app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                   //c.RoutePrefix = string.Empty; // or "swagger" if hosted under /swagger
                   c.RoutePrefix = "swagger"; // or "swagger" if hosted under /swagger
                });
            }
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

           app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
