using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces;
using MyApp.Application.Services.Categories.DTOs;

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
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var category = await _categoryService.GetAllAsync();

            return Ok(category);
        }
    }
}
