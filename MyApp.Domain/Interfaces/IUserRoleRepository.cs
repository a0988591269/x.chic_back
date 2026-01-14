namespace MyApp.Domain.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<string>> GetRolesByUserId(long userId);
    }
}