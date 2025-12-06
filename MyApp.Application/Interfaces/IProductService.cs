using MyApp.Application.Services.Products.DTOs;

namespace MyApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetBySlug(string slug);
    }
}
