using AutoMapper;
using Ma7ali.DashBoard.Data.Data.Contexts;
using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using Ma7ali.DashBoard.Data.Helper;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7ali.DashBoard.Service.Interfaces;
using Ma7aliDashBoard.Service.Dtos;
using Ma7aliDashBoard.Shared.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ma7aliDashBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly Ma7aliContext _ma7AliContext;

        public IMapper _Mapper { get; }

        public StoreController(IStoreService storeService, Ma7aliContext ma7AliContext, IMapper autoMapper, ILoggerFactory loggerFactory)
        {
            _storeService = storeService;
            _ma7AliContext = ma7AliContext;
            _Mapper = autoMapper;
        }

        // GET: api/Store
        [HttpGet("all")]
        public async Task<ActionResult<StoreDto>> GetAllStores()
        {
            return Ok(await _storeService.GetAllStores());

        }
        // POST: api/Store
        [HttpPost]
        public async Task<ActionResult<StoreDto>> CreateStore([FromForm] StoreCreateDto storeCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("InValid Model ");
            }
            var store = await _storeService.CreateStore(storeCreateDto);
            return CreatedAtAction(nameof(GetStoreById), new { id = store.Id }, store);
        }
        // GET: api/Store/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDto>> GetStoreById(int id)
        {
            return Ok(await _storeService.GetStoreById(id));
        }

        // PUT: api/Store/5
        [HttpPut("{id}")]
        public async Task<ActionResult<StoreDto>> UpdateStore(int id, [FromBody] StoreUpdateDto storeUpdateDto)
        {
            if (!ModelState.IsValid || id != storeUpdateDto.Id)

                return BadRequest("Invalid Store Data");

            return Ok(await _storeService.EditStore(storeUpdateDto));

        }
        // DELETE: api/Store/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStore(int id)
        {

            var deletedStore = await _storeService.DeleteStore(id);
            return NoContent();

        }

        [HttpGet("ByName")]
        public async Task<ActionResult<List<StoreDto>>> GetStoreByName([FromQuery] string name)
        {
           return await  _storeService.GetStoreByName(name);
        }

        //[HttpGet("name")]
        //public async Task<ActionResult<StoreDto>> GetStoreByName(string name)
        //{
        //    var store = await _ma7AliContext.Stores.
        //        Include(s => s.StoreProducts).
        //        Include(s => s.StoreCategories).
        //         Include(s => s.StoreProducts)
        //        .ThenInclude(p => p.Category).
        //         Include(s => s.StoreProducts)
        //        .ThenInclude(p => p.Images)
        //        .FirstOrDefaultAsync(s => s.StoreName.Contains(name));
        //    if (store == null)
        //    {
        //        return NotFound("No Store With this Name");
        //    }
        //    var StoreMapped = _Mapper.Map<Store, StoreDto>(store);
        //    StoreMapped.ProductCount = store.StoreProducts.Count;
        //    return Ok(StoreMapped);

        //}



        [HttpPost("Pagginated")]
        public async Task<ActionResult<PagedResult<StoreDto>>> GetPaged([FromBody] PagedRequestDto request)
        {
            var result = await _storeService.GetStoresPagedAsync(request);
            return Ok(result);
        }






        [HttpGet("ProductsBysoreId")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsInStore(int storeId)
        {

            var products = await _ma7AliContext.Products.Where(p => p.StoreId == storeId)

                .Include(x => x.Category).Include(p => p.Images)
                .ToListAsync();
            var ProductMapped = _Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            return Ok(ProductMapped);

        }
        [HttpGet("CategoriesBysoreId")]
        public async Task<ActionResult<IEnumerable<CategoryToReturnDto>>> GetCategoriesInStore(int storeId)
        {

            try
            {
                var categories = await _ma7AliContext.Categories.Where(s => s.StoreId == storeId).ToListAsync();

                var CategoryMapped = _Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryToReturnDto>>(categories);

                return Ok(CategoryMapped);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());

            }

        }






    }
}
