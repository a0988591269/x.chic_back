using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Products.Queries.GetProductBySlug;
using MyApp.Application.Features.Products.Queries.GetProductDetail;

namespace MyApp.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Product")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetProductDetail/{productId}")]
        public async Task<ActionResult<IEnumerable<GetProductDetailDto>>> GetProductDetail([FromRoute]long productId)
        {
            var query = new GetProductDetailQuery
            {
                Id = productId
            };
            var result = await _mediator.Send(query);

            return HandleResult(result);
        }

        [HttpGet("GetBySlug/{slug}")]
        public async Task<ActionResult<IEnumerable<GetProductBySlugDto>>> GetBySlug([FromRoute] string slug)
        {
            var query = new GetProductBySlugQuery
            {
                Slug = slug
            };
            var result = await _mediator.Send(query);

            return HandleResult(result);
        }

        //[Authorize(Roles = "Admin")]
        //public IActionResult CreateProduct()
        //{
        //    return Ok("");
        //}
    }
}