using MyApp.Domain.Models.Products;

namespace MyApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<GetProductBySlugModel>> GetProductBySlug(string slug);
        Task<GetProductDetailModel?> GetProductById(long Id);
    }
}