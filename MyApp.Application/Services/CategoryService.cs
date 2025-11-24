using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Repositories;

namespace MyApp.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var result = await _repo.GetAllAsync();

            // Entity -> DTO
            return result.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                CategoryEngName = c.CategoryEngName,
                Description = c.Description,
                Slug = c.Slug,
            });
        }
    }
}
