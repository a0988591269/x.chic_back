using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Categories.Queries.GetCategory;

namespace MyApp.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Category")]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCategoryDto>>> Get()
        {
            var query = new GetCategoryQuery();
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Data);
        }
    }
}
