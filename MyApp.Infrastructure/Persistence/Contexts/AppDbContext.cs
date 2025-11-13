using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        private string? _providerName;

        private string ProviderName
        {
            get
            {
                try
                {
                    return _providerName ??= Database.ProviderName ?? string.Empty;
                }
                catch
                {
                    return _providerName ?? "SqlServer";
                }
            }
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            _providerName = Database.ProviderName ?? string.Empty;
        }

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
        public DbSet<InventoryReservation> InventoryReservations => Set<InventoryReservation>();
        public DbSet<OutboxEvent> OutboxEvents => Set<OutboxEvent>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var et in modelBuilder.Model.GetEntityTypes())
            {
                var prop = et.FindProperty("RowVersion");
                if (prop != null && prop.ClrType == typeof(byte[]))
                {
                    modelBuilder.Entity(et.ClrType)
                        .Property<byte[]>("RowVersion")
                        .IsRowVersion()
                        .IsConcurrencyToken();
                }

                var createdAt = et.FindProperty("CreatedAt");
                if (createdAt != null && createdAt.ClrType == typeof(DateTime))
                {
                    modelBuilder.Entity(et.ClrType)
                        .Property<DateTime>("CreatedAt")
                        .HasDefaultValueSql(IsSqlServer() ? "GETUTCDATE()" : "CURRENT_TIMESTAMP");
                }
            }

            // Entity configurations
            ConfigureUserRoleAndAuth(modelBuilder);
            ConfigureCatalog(modelBuilder);
            ConfigureProducts(modelBuilder);
            ConfigureVariants(modelBuilder);
            ConfigureOrders(modelBuilder);
            ConfigurePayments(modelBuilder);
            ConfigureRefunds(modelBuilder);
            ConfigureShipments(modelBuilder);
            ConfigureOthers(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        #region Configs - Auth
        private void ConfigureUserRoleAndAuth(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("Users");
                b.HasKey(x => x.UserId);
                b.HasIndex(x => x.Email).IsUnique();
                b.Property(x => x.Email).HasMaxLength(320).IsRequired();
                b.Property(x => x.Name).HasMaxLength(200);
                b.Property(x => x.HashedPassword).HasMaxLength(200);
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.ToTable("Roles");
                b.HasKey(x => x.RoleId);
                b.Property(x => x.Name).HasMaxLength(100).IsRequired();
                b.Property(x => x.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                b.ToTable("UserRoles");
                b.HasKey(x => x.UserRoleId);
                b.HasIndex(x => new { x.UserId, x.RoleId }).IsUnique();
                b.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RolePermission>(b =>
            {
                b.ToTable("RolePermissions");
                b.HasKey(x => x.RolePermissionId);
                b.HasIndex(x => new { x.RoleId, x.Permission }).IsUnique();
                b.Property(x => x.Permission).HasMaxLength(200).IsRequired();
                b.HasOne(rp => rp.Role)
                 .WithMany(r => r.RolePermissions)
                 .HasForeignKey(rp => rp.RoleId)
                 .OnDelete(DeleteBehavior.Cascade);
            });
        }
        #endregion

        #region Configs - Catalog
        private void ConfigureCatalog(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(b =>
            {
                b.ToTable("Categories");
                b.HasKey(x => x.CategoryId);
                b.HasIndex(x => x.CategoryEngName).IsUnique();
                b.Property(x => x.CategoryEngName).HasMaxLength(100).IsRequired();
                b.Property(x => x.CategoryName).HasMaxLength(100).IsRequired();
                b.Property(x => x.Description).HasMaxLength(500);
            });
        }
        #endregion

        #region Configs - Products
        private void ConfigureProducts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(b =>
            {
                b.ToTable("Products");
                b.HasKey(x => x.ProductId);
                b.Property(x => x.ProductName).HasMaxLength(200).IsRequired();
                b.Property(x => x.ShortDescription).HasMaxLength(500);
                b.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductOption>(b =>
            {
                b.ToTable("ProductOptions");
                b.HasKey(x => x.ProductOptionId);
                b.Property(x => x.Name).HasMaxLength(100).IsRequired();
                b.HasOne(po => po.Product)
                    .WithMany(p => p.Options)
                    .HasForeignKey(po => po.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductOptionValue>(b =>
            {
                b.ToTable("ProductOptionValues");
                b.HasKey(x => x.ProductOptionValueId);
                b.Property(x => x.Value).HasMaxLength(200).IsRequired();
                b.HasOne(v => v.ProductOption)
                    .WithMany(po => po.Values)
                    .HasForeignKey(v => v.ProductOptionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
        #endregion

        #region Configs - Variants
        private void ConfigureVariants(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductVariant>(b =>
            {
                b.ToTable("ProductVariants");
                b.HasKey(x => x.ProductVariantId);
                b.HasIndex(x => x.Sku).IsUnique();
                b.Property(x => x.Sku).HasMaxLength(100).IsRequired();
                b.Property(x => x.Price).HasColumnType(GetDecimalColumnType()).IsRequired();
                b.Property(x => x.StockQty).HasDefaultValue(0);
                b.HasOne(v => v.Product)
                    .WithMany(p => p.Variants)
                    .HasForeignKey(v => v.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductVariantOptionValue>(b =>
            {
                b.ToTable("ProductVariantOptionValues");
                b.HasKey(x => x.ProductVariantOptionValueId);
                b.HasIndex(x => new { x.ProductVariantId, x.ProductOptionValueId }).IsUnique();
                b.HasOne(m => m.ProductVariant)
                    .WithMany(v => v.OptionValues)
                    .HasForeignKey(m => m.ProductVariantId)
                    .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(m => m.ProductOptionValue)
                    .WithMany(pov => pov.ProductVariantOptionValues)
                    .HasForeignKey(m => m.ProductOptionValueId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductVariantImage>(b =>
            {
                b.ToTable("ProductVariantImages");
                b.HasKey(x => x.ProductVariantImageId);
                b.Property(x => x.ImageUrl).HasMaxLength(2000).IsRequired();
                b.HasOne(i => i.ProductVariant)
                    .WithMany(v => v.Images)
                    .HasForeignKey(i => i.ProductVariantId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
        #endregion

        #region Configs - Orders
        private void ConfigureOrders(ModelBuilder modelBuilder)
        {
            // Orders
            modelBuilder.Entity<Order>(b =>
            {
                b.ToTable("Orders");
                b.HasKey(x => x.OrderId);
                b.HasIndex(x => x.OrderNumber).IsUnique();
                b.Property(x => x.OrderNumber).HasMaxLength(50);
                b.Property(x => x.TotalAmount).HasColumnType(GetDecimalColumnType()).IsRequired();
                b.Property(x => x.Currency).HasMaxLength(3).IsRequired().HasDefaultValue("TWD");
                b.Property(x => x.ItemsSnapshot).HasColumnType("nvarchar(max)").IsRequired();
                b.HasMany(o => o.OrderItems)
                    .WithOne()
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // OrderItems
            modelBuilder.Entity<OrderItem>(b =>
            {
                b.ToTable("OrderItems");
                b.HasKey(x => x.OrderItemId);
                b.Property(x => x.PriceAtPurchase).HasColumnType(GetDecimalColumnType()).IsRequired();
                b.Property(x => x.Subtotal).HasColumnType(GetDecimalColumnType()).IsRequired();
                b.Property(x => x.Title).HasMaxLength(200);
                b.Property(x => x.Sku).HasMaxLength(100);
            });
        }
        #endregion

        #region Configs - Payments
        private void ConfigurePayments(ModelBuilder modelBuilder)
        {
            // Payments
            modelBuilder.Entity<Payment>(b =>
            {
                b.ToTable("Payments");
                b.HasKey(x => x.PaymentId);
                b.HasIndex(x => x.GatewayPaymentId);
                b.Property(x => x.Gateway).HasMaxLength(100).IsRequired();
                b.Property(x => x.GatewayPaymentId).HasMaxLength(200).IsRequired();
                b.Property(x => x.Amount).HasColumnType(GetDecimalColumnType()).IsRequired();
                b.HasOne<Order>().WithMany().HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
            });
        }
        #endregion

        #region Configs - Refunds
        private void ConfigureRefunds(ModelBuilder modelBuilder)
        {
            // Refunds
            modelBuilder.Entity<Refund>(b =>
            {
                b.ToTable("Refunds");
                b.HasKey(x => x.RefundId);
                b.Property(x => x.Amount).HasColumnType(GetDecimalColumnType()).IsRequired();
                b.Property(x => x.Reason).HasMaxLength(500);
                b.Property(x => x.Status).IsRequired();

                b.HasOne<Order>()
                    .WithMany()
                    .HasForeignKey(x => x.OrderId)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne<Payment>()
                    .WithMany()
                    .HasForeignKey(x => x.PaymentId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
        #endregion

        #region Configs - Shipments
        private void ConfigureShipments(ModelBuilder modelBuilder)
        {
            // Shipments
            modelBuilder.Entity<Shipment>(b =>
            {
                b.ToTable("Shipments");
                b.HasKey(x => x.ShipmentId);
                b.Property(x => x.Carrier).HasMaxLength(100).IsRequired();
                b.Property(x => x.TrackingNo).HasMaxLength(200);
                b.Property(x => x.Status).IsRequired();
                b.HasOne<Order>().WithMany().HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
            });
        }
        #endregion

        #region Configs - Inventory / Outbox / Audit
        private void ConfigureOthers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryReservation>(b =>
            {
                b.ToTable("InventoryReservations");
                b.HasKey(x => x.ReservationId);
                b.HasIndex(x => new { x.ProductVariantId, x.Status, x.ExpiresAt });
                b.Property(x => x.ReservationQty).IsRequired();
            });

            modelBuilder.Entity<OutboxEvent>(b =>
            {
                b.ToTable("OutboxEvents");
                b.HasKey(x => x.OutboxId);
                b.Property(x => x.AggregateType).HasMaxLength(100);
                b.Property(x => x.EventType).HasMaxLength(100);
                b.Property(x => x.Payload).HasColumnType("nvarchar(max)").IsRequired();
                b.HasIndex(x => x.ProcessedAt);
            });

            modelBuilder.Entity<AuditLog>(b =>
            {
                b.ToTable("AuditLogs");
                b.HasKey(x => x.AuditId);
                b.Property(x => x.EntityType).HasMaxLength(100).IsRequired();
                b.Property(x => x.Operation).HasMaxLength(50).IsRequired();
                b.Property(x => x.PerformedBy).HasMaxLength(200);
                b.Property(x => x.PayloadBefore).HasColumnType("nvarchar(max)");
                b.Property(x => x.PayloadAfter).HasColumnType("nvarchar(max)");
                b.HasIndex(x => new { x.EntityType, x.EntityId });
            });
        }
        #endregion

        #region Helpers
        private bool IsSqlServer() => ProviderName.Contains("SqlServer", StringComparison.OrdinalIgnoreCase);
        private bool IsPostgres() => ProviderName.Contains("Npgsql", StringComparison.OrdinalIgnoreCase);

        private string GetDecimalColumnType()
            => IsSqlServer() ? "decimal(18,2)" : "numeric(18,2)";
        #endregion
    }
}