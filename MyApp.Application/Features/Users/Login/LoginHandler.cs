using Dapper;
using MediatR;
using MyApp.Application.Commons.Results;
using MyApp.Application.Features.Categories.Queries.GetCategory;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.JWT;
using MyApp.Infrastructure.Persistence.Contexts;
using MyApp.Shared.Helpers;
using System.Security.Claims;

namespace MyApp.Application.Features.Users.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, Result<LoginDto>>
    {
        private readonly IConnectionFactory _factory;
        private readonly IJwtTokenGenerator _jwt;

        public LoginHandler(
            IConnectionFactory factory,
            IJwtTokenGenerator jwt)
        {
            _factory = factory;
            _jwt = jwt;
        }

        public async Task<Result<LoginDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            using var conn = _factory.GetConnection();

            // 驗證帳密
            var user = await conn.QuerySingleOrDefaultAsync<User>(
                @"
                    SELECT * 
                    FROM Users 
                    WHERE Email = @Email AND Status = 1
                ",
                new { request.Email });

            if (user == null || !PasswordHasher.Verify(request.Password, user.HashedPassword))
                return Result<LoginDto>.NotFound("帳號或密碼錯誤");

            // 撈 Roles
            var roles = await conn.QueryAsync<string>(
                @"
                    SELECT r.Name
                    FROM UserRoles ur
                    JOIN Roles r ON ur.RoleId = r.RoleId
                    WHERE ur.UserId = @UserId
                ",
                new { user.UserId });

            // 撈 Permissions（⭐ 關鍵）
            var permissions = await conn.QueryAsync<string>(
                @"
                    SELECT DISTINCT rp.Permission
                    FROM UserRoles ur
                    JOIN RolePermissions rp ON ur.RoleId = rp.RoleId
                    WHERE ur.UserId = @UserId
                ",
                new { user.UserId });

            // 組裝 Claims
            var claims = BuildClaims(user, roles, permissions);

            // 發 JWT
            var token = _jwt.GenerateToken(claims);

            return Result<LoginDto>.Success(new LoginDto(token));
        }

        private static List<Claim> BuildClaims(User user, IEnumerable<string> roles, IEnumerable<string> permissions)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim("user_uuid", user.UserUuid.ToString()),
                new Claim(ClaimTypes.Name, user.Name ?? ""),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("tier", user.Tier.ToString())
            };

            // 多角色
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            foreach (var permission in permissions)
            {
                claims.Add(new Claim("permission", permission));
            }

            return claims;
        }
    }
}