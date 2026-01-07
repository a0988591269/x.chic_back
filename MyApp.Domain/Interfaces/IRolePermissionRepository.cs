namespace MyApp.Domain.Interfaces
{
    public interface IRolePermissionRepository
    {
        Task<IEnumerable<string>> GetRolePermissionByUserId(long UserId);
    }
}