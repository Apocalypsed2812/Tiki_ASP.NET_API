using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Models;
using ToDoAPI.Repositories.CategoryRepository;
using ToDoAPI.Specifications;
using ToDoAPI.Specifications.CategorySpecification;
using ToDoAPI.Specifications.ProductSpecification;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                return Ok(await _categoryRepo.GetAllCategoryAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryRepo.GetCategoryAsync(id);
            return category == null ? NotFound() : Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCategory(CategoryModel model)
        {
            try
            {
                var newCategoryId = await _categoryRepo.AddCategoryAsync(model);
                var newCategory = await _categoryRepo.GetCategoryAsync(newCategoryId);
                return newCategory == null ? NotFound() : Ok(newCategory);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return NotFound();
                }
                await _categoryRepo.UpdateCategoryAsync(id, model);
                var categories = await _categoryRepo.GetAllCategoryAsync();
                return Ok(categories);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryRepo.DeleteCategoryAsync(id);
                var categories = await _categoryRepo.GetAllCategoryAsync();
                return Ok(categories);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("get-category-by-name")]
        public async Task<IActionResult> GetProductGreaterThanPrice(string name)
        {
            try
            {
                var nameCondition = new CategorySpecification(name);
                var result = await _categoryRepo.GetCategoryByName(nameCondition);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Has Orrcurred Error: {ex.Message}");
            }
        }
    }
}