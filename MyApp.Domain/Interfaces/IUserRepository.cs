using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface IUserRepository 
    {
        Task<User?> GetUserByEmail(string Email);
    }
}