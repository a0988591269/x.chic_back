using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var result = await _repo.GetAllAsync();
            // Entity -> DTO
            return result.Select(p => new ProductDto
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
                CategoryId = p.CategoryId,
                ImageId = p.ImageId,
                Category = new CategoryDto
                {
                    CategoryId = p.Category.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    CategoryEngName = p.Category.CategoryEngName
                }
            });
        }

        public async Task<ProductDto?> GetProductAsync(int productId)
        {
            var result = await _repo.GetProductAsync(productId);
            // Entity -> DTO
            if (result == null) return null;
            return new ProductDto
            {
                ProductId = result.ProductId,
                ProductName = result.ProductName,
                Intro = result.Intro,
                Description = result.Description,
                Notice = result.Notice,
                Price = result.Price,
                Discount = result.Discount,
                Stock = result.Stock,
                SalesVolume = result.SalesVolume,
                CategoryId = result.CategoryId
            };
        }

        public async Task<IEnumerable<ProductDto>> GetProductByCategoryId(int categoryId)
        {
            var result = await _repo.GetProductByCategoryIdAsync(categoryId);
            // Entity -> DTO
            return result.Select(p => new ProductDto
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
                CategoryId = p.CategoryId,
                ImageId = p.ImageId,
                Category = new CategoryDto
                {
                    CategoryId = p.Category.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    CategoryEngName = p.Category.CategoryEngName
                }
            });
        }
        
    }
}
