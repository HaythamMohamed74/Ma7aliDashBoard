using Ma7ali.DashBoard.Data.Data.Contexts;
using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Repository
{
    public  class SeedingContext
    {
        public static  async Task SeedingAsync(Ma7aliContext ma7AliContext,ILoggerFactory loggerFactory)
        {
            var log = loggerFactory.CreateLogger<SeedingContext>();
            try
            {
                log.LogInformation("Seeding Data Started...");
                var Brands = File.ReadAllText("../Ma7ali.DashBoard.Repository/SeedData/BrandSeed.json");
                var SerlizedBrands = JsonSerializer.Deserialize<List<Brand>>(Brands);

                if (SerlizedBrands is not null && !ma7AliContext.Brands.Any())
                {
                    await ma7AliContext.Brands.AddRangeAsync(SerlizedBrands);

                }

                var Categories = File.ReadAllText("../Ma7ali.DashBoard.Repository/SeedData/CategorySeed.json");
                var SerlizedCategories = JsonSerializer.Deserialize<List<Category>>(Categories);

                if (SerlizedCategories is not null&& !ma7AliContext.Categories.Any())
                {
                    await ma7AliContext.Categories.AddRangeAsync(SerlizedCategories);

                }
                var Stores = File.ReadAllText("../Ma7ali.DashBoard.Repository/SeedData/StoreSeeding.json");
                var SerlizedStores = JsonSerializer.Deserialize<List<Store>>(Stores);

                if (SerlizedStores is not null && !ma7AliContext.Stores.Any())
                {
                    await ma7AliContext.Stores.AddRangeAsync(SerlizedStores);

                }
                var Products = File.ReadAllText("../Ma7ali.DashBoard.Repository/SeedData/ProductSeed.json");
                var SerlizedProduct = JsonSerializer.Deserialize<List<Product>>(Products);

                if (SerlizedProduct is not null && !ma7AliContext.Products.Any())
                {
                    await ma7AliContext.Products.AddRangeAsync(SerlizedProduct);

                }





                await ma7AliContext.SaveChangesAsync();
                log.LogInformation("Seeding Data Completed...");
            }
            catch (Exception e)
            {


                //var log=loggerFactory.CreateLogger<SeedingContext>();
                log.LogError("An Error Occured During Seeding..."+e.Message.ToString());
                

            }

        }
    }
}
