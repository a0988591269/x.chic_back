using Microsoft.AspNetCore.Mvc;
using MyApp.API.Models;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;

namespace MyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> Get()
        {
            var products = await _productService.GetAllAsync();
            // DTO -> Model
            var response = products.Select(p => new ProductModel
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Intro = p.Intro,
                Description = p.Description,
                Notice = p.Notice,
                Price = p.Price,
                Discount = p.Discount,
                Stock = p.Stock,
                SalesVolume = p.SalesVolume,
                CategoryId = p.CategoryId
            });
            return Ok(response);
        }

        [HttpGet("{CategoryId}")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> Get(int CategoryId)
        {
            var products = await _productService.GetAllAsync();
            // DTO -> Model
            var response = products.Select(p => new ProductModel
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Intro = p.Intro,
                Description = p.Description,
                Notice = p.Notice,
                Price = p.Price,
                Discount = p.Discount,
                Stock = p.Stock,
                SalesVolume = p.SalesVolume,
                CategoryId = p.CategoryId
            });
            return Ok(response);
        }
    }
}
