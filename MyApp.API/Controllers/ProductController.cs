using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces;
using MyApp.Application.Services.Products.DTOs;

namespace MyApp.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Product")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetBySlug")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetBySlug(string slug)
        {
            var products = await _productService.GetBySlug(slug);

            return Ok(products);
        }
    }
}