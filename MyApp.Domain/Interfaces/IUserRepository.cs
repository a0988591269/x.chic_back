using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface IUserRepository 
    {
        Task<User?> GetUserByEmail(string email);
        Task<bool> IsEmailUniqueAsync(string email, CancellationToken token);
        Task AddAsync(User user, CancellationToken token);
        Task<IEnumerable<User>> GetAllUsers();
    }
}