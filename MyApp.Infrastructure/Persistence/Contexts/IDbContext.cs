using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MyApp.Domain.Entities;

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
        // DbSets
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductOption> ProductOptions => Set<ProductOption>();
        public DbSet<ProductOptionValue> ProductOptionValues => Set<ProductOptionValue>();
        public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
        public DbSet<ProductVariantOptionValue> ProductVariantOptionValues => Set<ProductVariantOptionValue>();
        public DbSet<ProductVariantImage> ProductVariantImages => Set<ProductVariantImage>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Refund> Refunds => Set<Refund>();
        public DbSet<Shipment> Shipments => Set<Shipment>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<InventoryReservation> InventoryReservations => Set<InventoryReservation>();
        public DbSet<OutboxEvent> OutboxEvents => Set<OutboxEvent>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    }
}
