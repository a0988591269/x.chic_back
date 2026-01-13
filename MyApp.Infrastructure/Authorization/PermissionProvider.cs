namespace MyApp.Infrastructure.Authorization
{
    public class PermissionProvider
    {
        public Task<HashSet<string>> GetForUserIdAsync(Guid userId)
        {
            // TODO：在這裡，您將實現獲取權限的邏輯。
            HashSet<string> permissionsSet = [];

            return Task.FromResult(permissionsSet);
        }
    }
}
