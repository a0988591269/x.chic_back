using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.Infrastructure.Persistence.Contexts;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Persistence.Seed.Seeders
{
    public static class UsersAndRolesSeeder
    {
        public static async Task Run(AppDbContext context, ILogger logger)
        {
            if (await context.Roles.AnyAsync()) return;

            // 建立角色
            var adminRole = new Role
            {
                Name = "Admin",
                Description = "System Administrator"
            };

            var customerRole = new Role
            {
                Name = "Customer",
                Description = "Regular Customer"
            };

            context.Roles.AddRange(adminRole, customerRole);
            await context.SaveChangesAsync();

            // 建立權限（對應 RolePermission）
            var permissions = new[]
            {
                "Product.Read",
                "Product.Create",
                "Product.Update",
                "Product.Delete",
                "Order.Read",
                "Order.Manage"
            };

            var adminPermissions = permissions.Select(p => new RolePermission
            {
                RoleId = adminRole.RoleId,
                Permission = p
            }).ToList();

            context.RolePermissions.AddRange(adminPermissions);
            await context.SaveChangesAsync();

            // 建立管理員
            var admin = new User
            {
                Email = "admin@myapp.com",
                Name = "Super Admin",
                HashedPassword = "admin123" // 🚨 DEMO only, 請改成 hash
            };

            context.Users.Add(admin);
            await context.SaveChangesAsync();

            // 建立關聯
            context.UserRoles.Add(new UserRole
            {
                UserId = admin.UserId,
                RoleId = adminRole.RoleId
            });

            await context.SaveChangesAsync();

            logger.LogInformation("👑 Seeded Roles + Permissions + Admin User");
        }
    }
}