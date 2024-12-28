using AutoMapper;
using Ma7ali.DashBoard.Data.Data.Contexts;
using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7aliDashBoard.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace Ma7aliDashBoard.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly Ma7aliContext _ma7AliContext;
        private readonly ILoggerFactory _loggerFactory;

        public IMapper _Mapper { get; }

        public StoreController(Ma7aliContext ma7AliContext , IMapper autoMapper , ILoggerFactory loggerFactory)
        {
            _ma7AliContext = ma7AliContext;
            _Mapper = autoMapper;
            _loggerFactory = loggerFactory;
        }


        [HttpGet("id")]
        public async Task<ActionResult<StoreDto>> GetStore(int id)
        {
            try
            {
              

                var store = await _ma7AliContext.Stores
                                .Include(x => x.StoreCategories)
                                .Include(x => x.StoreProducts)
                                    .ThenInclude(p => p.Category)
                                .Include(x => x.StoreProducts)
                                .ThenInclude(p=>p.Images).
                                Include(x => x.StoreProducts).
                                    ThenInclude(p => p.Brand)
                                .FirstOrDefaultAsync(s => s.Id == id);

                
              
                if (store == null)
                {
                    return NotFound();
                }
                var StoreMapped = _Mapper.Map<Store, StoreDto>(store);
                StoreMapped.ProductCount = store.StoreProducts.Count;


                return Ok(StoreMapped);
            }
            catch (Exception e)
            {
               
               return BadRequest(e.Message);


            }
            

        }


        [HttpGet] public async Task<ActionResult<StoreDto>>GetStoreByName(string name)
        {
            var store = await _ma7AliContext.Stores.
                Include(s=>s.StoreProducts)
                .ThenInclude(p=>p.Brand).
                Include(s=>s.StoreCategories).
                 Include(s => s.StoreProducts)
                .ThenInclude(p => p.Category).
                 Include(s => s.StoreProducts)
                .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(s => s.StoreName.Contains(name));
            if (store == null)
            {
                return NotFound();
            }
            var StoreMapped = _Mapper.Map<Store, StoreDto>(store);
            StoreMapped.ProductCount = store.StoreProducts.Count;
            return Ok(StoreMapped);

        }


        [HttpPost]
        public async Task<ActionResult> CreateStore([FromBody] StoreCreateDto storeCreateDto)
        {
            var log = _loggerFactory.CreateLogger<StoreController>();
            try
            {
                log.LogInformation("Create Store Started...");

                var store = _Mapper.Map<StoreCreateDto, Store>(storeCreateDto);
                var storeMapped = await _ma7AliContext.Stores.AddAsync(store);

                await _ma7AliContext.SaveChangesAsync();

                var storeDto = _Mapper.Map<Store, StoreDto>(storeMapped.Entity);

                log.LogInformation("Create Store Completed...");
                return CreatedAtAction(nameof(GetStore), new { id = storeDto.Id }, storeDto);
            }
            catch (Exception e)
            {
                log.LogError("An Error Occurred During Creating Store: {ErrorMessage}", e.Message);
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("id")]
        public async Task<ActionResult> DeleteStore(int id)
        {
            try
            {
                var store = await _ma7AliContext.Stores.FindAsync(id);
                if (store == null)
                {
                    return NotFound();
                }
                _ma7AliContext.Stores.Remove(store);
                await _ma7AliContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message.ToString());
            }

        }




        [HttpGet("storeId")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsInStore(int storeId)
        {

            var products =  await _ma7AliContext.Products.Where(p => p.StoreId == storeId)
                .Include(x=>x.Brand)
                .Include(x=>x.Category).Include(p=>p.Images)
                .ToListAsync();
         var ProductMapped=  _Mapper.Map<IEnumerable <Product>,IEnumerable <ProductDto>>(products);
            return Ok(ProductMapped);

        }
        [HttpGet("storeId")]
        public async Task <ActionResult<IEnumerable<CategoryDto>>> GetCategoriesInStore(int storeId)
        {

            try
            {
                var categories = await _ma7AliContext.Categories.Where(s => s.StoreId == storeId).ToListAsync();

                var CategoryMapped = _Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);

                return Ok(CategoryMapped);
            }
            catch (Exception e )
            {
                return BadRequest(e.ToString());
                
            }

        }


     
      

      
    }
}
