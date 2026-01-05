using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Infrastructure.Persistence.Contexts
{
    public interface IDbContext : IDisposable
    {
        // 進階（Database/Transaction）
        DatabaseFacade Database { get; }
        // 基本 EF Methods
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
