using AutoMapper;
using Ma7ali.DashBoard.Data.Data.Contexts;
using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7ali.DashBoard.Service.Interfaces;
using Ma7aliDashBoard.Service.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ma7aliDashBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreProductController : ControllerBase
    {
        private readonly Ma7aliContext _ma7AliContext;
        private readonly IMapper _Mapper;
        private readonly IProductService _productService;
    

        public StoreProductController(Ma7aliContext ma7AliContext, IMapper mapper, IProductService productService, IAuthService authService)
        {
            _ma7AliContext = ma7AliContext;
            _Mapper = mapper;
            _productService = productService;
         
        }



        [HttpPost("add-product")]
        public async Task<ActionResult> CreateProduct([FromForm] ProductCreationDto productDto)
        {

            var result = await _productService.AddProduct(productDto);
            return CreatedAtAction(nameof(CreateProduct), new { id = result.Id }, result);

        }

        [HttpPut("update-product")]
        public async Task<ActionResult<UpdateProductDto>> UpdateProduct(int id, UpdateProductDto productDto)
        {
            try
            {
                var product = await _ma7AliContext.Products.FindAsync(id);
                if (product is null)
                {
                    return NotFound("Product Not found");
                }

                _Mapper.Map(productDto, product); // Apply the mapped properties to the existing product entity
                _ma7AliContext.Products.Update(product);
                await _ma7AliContext.SaveChangesAsync();

                var updatedProductDto = _Mapper.Map<Product, ProductDto>(product); // Map the updated product entity to ProductDto
                return Ok(updatedProductDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }


        [HttpGet("Best-seller")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> BestSeller()
        {
            var bestSellersMapped = await _productService.GetBestSallerProducts();
            return Ok(bestSellersMapped);
        }

        [HttpGet("Top-rated")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> TopRated()
        {
            var bestSellersMapped = await _productService.GetTopRatedProducts();
            return Ok(bestSellersMapped);
        }

        [HttpGet("All-products")]
        public ActionResult<ProductDto> GetProducts()
        {

            var products = _ma7AliContext.Products.Include(p => p.Images).Include(p => p.Category).ToList();
            var productsMapped = _Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            return Ok(productsMapped);
            //var productsMapped = products.Select(p => new ProductDto
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Description = p.Description,
            //    Price = p.Price,
            //    Stock = p.Stock,
            //    CategoryId = p.CategoryId,
            //    CategoryName = p.Category?.Name,
            //    StoreId = p.StoreId,
            //    CreationTime = p.CreatedAt,
            //    //LastUpdateTime = p.la,
            //    Images = p.Images.Select(img => img.ImageUrl.Replace("\\", "/")).ToList()
            //});

            //return Ok(productsMapped);
        }


        [HttpGet("Get-page")]
        public async Task<ActionResult> GetPage(string search, int page = 1, int size = 10)
        {
            var result = await _productService.GetSortedFilteredPagedAsync(search, page, size);

            return Ok(result);
        }

        //[HttpGet]
        //public async Task<ActionResult<ProductDto>> GetAllProducts()
        //{
        //    //var products = _ma7AliContext.Products.Include(p => p.Images).Include(x => x.Category).ToList();
        //    //var productsMapped = _Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        //    //return Ok(productsMapped);
        //    var products = await _productService.GetAllProduct();
        //    return Ok(products);

        //}


        [HttpDelete("Remove-product")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                var product = _ma7AliContext.Products.Find(id);
                if (product == null)
                {
                    return NotFound();
                }
                _ma7AliContext.Products.Remove(product);
                _ma7AliContext.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message.ToString());
            }


        }


        [HttpGet($"Product-id")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }



       

    }
}
