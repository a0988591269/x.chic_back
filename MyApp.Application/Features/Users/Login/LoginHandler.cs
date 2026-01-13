using MediatR;
using MyApp.Application.Commons.Interfaces.Authentication;
using MyApp.Application.Commons.Interfaces.JWT;
using MyApp.Application.Commons.Results;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System.Security.Claims;

namespace MyApp.Application.Features.Users.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, Result<LoginDto>>
    {
        private readonly IJwtTokenGenerator _jwt;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public LoginHandler(
            IJwtTokenGenerator jwt,
            IPasswordHasher passwordHasher,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IRolePermissionRepository rolePermissionRepository)
        {
            _jwt = jwt;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<Result<LoginDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            // 驗證帳密
            var user = await _userRepository.GetUserByEmail(request.Email);

            if (user == null || !_passwordHasher.Verify(request.Password, user.HashedPassword ?? ""))
                return Result<LoginDto>.NotFound("帳號或密碼錯誤");

            // 撈 Roles
            var roles = await _roleRepository.GetRolesByUserId(user.UserId);

            // 撈 Permissions（⭐ 關鍵）
            var permissions = await _rolePermissionRepository.GetRolePermissionByUserId(user.UserId);

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