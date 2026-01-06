using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
    }
}
