using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Products.Queries.GetProductById;
using MyApp.Application.Features.Products.Queries.GetProductBySlug;

namespace MyApp.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Product")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<IEnumerable<GetProductByIdDto>>> GetById([FromRoute]long productId)
        {
            var query = new GetProductByIdQuery
            {
                Id = productId
            };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Data);
        }

        [HttpGet("GetBySlug")]
        public async Task<ActionResult<IEnumerable<GetProductBySlugDto>>> GetBySlug(string slug)
        {
            var query = new GetProductBySlugQuery
            {
                Slug = slug
            };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Data);
        }
    }
}