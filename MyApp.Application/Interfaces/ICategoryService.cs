using MyApp.Application.DTOs;

namespace MyApp.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
    }
}
