using Microsoft.AspNetCore.Mvc;
using MyApp.API.Models;
using MyApp.Application.Interfaces;

namespace MyApp.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Category")]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> Get()
        {
            var category = await _categoryService.GetAllAsync();

            // DTO -> Model
            var response = category.Select(c => new CategoryModel
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                CategoryEngName = c.CategoryEngName,
                CategoryUrl = c.CategoryUrl
            });

            return Ok(response);
        }
    }
}
