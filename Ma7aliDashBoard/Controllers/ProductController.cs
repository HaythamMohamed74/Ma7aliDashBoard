using AutoMapper;
using Ma7ali.DashBoard.Data.Data.Contexts;
using Ma7ali.DashBoard.Data.Entities.ProductEntities;
using Ma7ali.DashBoard.Data.Entities.StoreEntities;
using Ma7ali.DashBoard.Service.Dtos;
using Ma7aliDashBoard.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Ma7aliDashBoard.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StoreProductController : ControllerBase
    {
        private readonly Ma7aliContext _ma7AliContext;
        private readonly IMapper _Mapper;

        public StoreProductController(Ma7aliContext ma7AliContext,IMapper mapper)
        {
            _ma7AliContext = ma7AliContext;
            _Mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto categoryCreateDto)
        {
            try
            {
                var category = await _ma7AliContext.Categories.AddAsync(_Mapper.Map<CategoryDto, Category>(categoryCreateDto));

                await _ma7AliContext.SaveChangesAsync();

                return Ok(category.Entity);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message.ToString());
            }

        }


        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductCreationDto productDto)
        {
            try
            {
             
                var product = _Mapper.Map<ProductCreationDto, Product>(productDto);
                if (product is null)
                {
                    return NotFound(" Not Found Product To Add ");
                }
                await _ma7AliContext.Products.AddAsync(_Mapper.Map<ProductCreationDto, Product>(productDto));
              await  _ma7AliContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message.ToString());
            }

        }
        [HttpPut]
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




        [HttpGet]
        public ActionResult<ProductDto> GetProducts()
        {

            var products = _ma7AliContext.Products.Include(p=>p.Images).Include(x => x.Brand).Include(x => x.Category).ToList();
            var productsMapped = _Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            return Ok(productsMapped);

        }

        [HttpGet]
        public ActionResult<CategoryDto> GetCategories()
        {

            var categories = _ma7AliContext.Categories.ToList();
            var CategoryMapped = _Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);
            return Ok(CategoryMapped);

        }
        [HttpDelete]
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
    }
}
