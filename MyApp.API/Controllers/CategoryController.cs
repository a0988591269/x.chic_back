using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Categories.Queries;
using MyApp.Application.Interfaces;

namespace MyApp.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Category")]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryService;

        public CategoryController(ICategoryRepository categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCategoryDto>>> Get()
        {
            var category = await _categoryService.GetAllAsync();

            return Ok(category);
        }
    }
}
