using Microsoft.AspNetCore.Mvc;
using MyApp.API.Models;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Application.Services;

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

        [HttpGet("{productId}")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> Get(int productId)
        {
            var product = await _productService.GetProductAsync(productId);
            // DTO -> Model
            if (product == null)
                return NotFound($"Product with ID {productId} not found.");
            var response = new ProductModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Intro = product.Intro,
                Description = product.Description,
                Notice = product.Notice,
                Price = product.Price,
                Discount = product.Discount,
                Stock = product.Stock,
                SalesVolume = product.SalesVolume,
                CategoryId = product.CategoryId
            };
            return Ok(response);
        }

        [HttpGet("ProductByCategoryId/{categoryId}")]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetProductByCategoryId(int categoryId)
        {
            var productList = await _productService.GetProductByCategoryId(categoryId);

            // DTO -> Model
            var response = productList.Select(p => new ProductModel
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
