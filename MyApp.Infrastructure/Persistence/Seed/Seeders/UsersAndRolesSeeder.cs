using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.Infrastructure.Persistence.Contexts;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Authentication;

namespace MyApp.Infrastructure.Persistence.Seed.Seeders
{
    public static class UsersAndRolesSeeder
    {
        public static async Task Run(Contexts.AppDbContext context, ILogger logger)
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

            // 建立管理者權限（對應 RolePermission）
            var adminPermissions = new[]
            {
                "Product.Read",
                "Product.Create",
                "Product.Update",
                "Product.Delete",
                "Order.Read",
                "Order.Manage",
                "Admin.Access"
            };

            var adminRolePermissions = adminPermissions.Select(p => new RolePermission
            {
                RoleId = adminRole.RoleId,
                Permission = p
            }).ToList();

            // 建立客戶權限（對應 RolePermission）
            var cusPermissions = new[]
            {
                "Product.Read",
                "Order.Read"
            };

            var cusRolePermissions = cusPermissions.Select(p => new RolePermission
            {
                RoleId = customerRole.RoleId,
                Permission = p
            }).ToList();

            var mixPermissions = new List<RolePermission>();
            mixPermissions.AddRange(adminRolePermissions);
            mixPermissions.AddRange(cusRolePermissions);

            context.RolePermissions.AddRange(mixPermissions);
            await context.SaveChangesAsync();

            // 建立管理員
            var admin = new User
            {
                Email = "admin@myapp.com",
                Name = "Super Admin",
                HashedPassword = PasswordHasher.Hash("12345678")
            };

            // 建立會員
            var customer = new User
            {
                Email = "customer@myapp.com",
                Name = "Customer",
                HashedPassword = PasswordHasher.Hash("12345678")
            };

            context.Users.AddRange(admin, customer);
            await context.SaveChangesAsync();

            // 建立關聯
            var adminUserRole = new UserRole
            {
                UserId = admin.UserId,
                RoleId = adminRole.RoleId
            };

            var customerUserRole = new UserRole
            {
                UserId = customer.UserId,
                RoleId = customerRole.RoleId
            };

            context.UserRoles.AddRange(adminUserRole, customerUserRole);
            await context.SaveChangesAsync();

            logger.LogInformation("👑 Seeded Roles + Permissions + User");
        }
    }
}