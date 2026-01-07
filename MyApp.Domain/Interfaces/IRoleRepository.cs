using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<string>> GetRolesByUserId(long UserId);
    }
}