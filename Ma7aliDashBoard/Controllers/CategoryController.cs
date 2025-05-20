using Ma7ali.DashBoard.Service.Dtos;
using Ma7ali.DashBoard.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ma7aliDashBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }       


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryToReturnDto>>> GetAllCategories()
        {

            var categories = await _categoryService.GetAllCategory();
            return Ok(categories);


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryToReturnDto>> GetCategoryById(int id)
        {

            var category = await _categoryService.GetCategoryById(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryToCreateDto>> CreateCategory([FromForm] CategoryToCreateDto categoryToCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("InValid Model");

            var category = await _categoryService.CreateCategory(categoryToCreateDto);
            //return Created();
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);

        }
        [HttpDelete ("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
           await _categoryService.DeleteCategory(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryToReturnDto>> EditCategory(int id, [FromBody]CategoryToEditDto categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest("Category ID mismatch.");
            }
            var updatedCategory = await _categoryService.EditCategory(categoryDto);
            return Ok(updatedCategory);
        }
    }
}
