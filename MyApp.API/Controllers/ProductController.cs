using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Products.Queries;
using MyApp.Application.Interfaces;

namespace MyApp.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Product")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productService;

        public ProductController(IProductRepository productService)
        {
            _productService = productService;
        }

        [HttpGet("GetBySlug")]
        public async Task<ActionResult<IEnumerable<GetProductBySlugDto>>> GetBySlug(string slug)
        {
            var products = await _productService.GetBySlug(slug);

            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
    }
}