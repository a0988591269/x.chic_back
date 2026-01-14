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
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public LoginHandler(
            IJwtTokenGenerator jwt,
            IPasswordHasher passwordHasher,
            IUserRepository userRepository,
            IUserRoleRepository userRoleRepository,
            IRolePermissionRepository rolePermissionRepository)
        {
            _jwt = jwt;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<Result<LoginDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            // 驗證帳密
            var user = await _userRepository.GetUserByEmail(request.Email);

            if (user == null)
            {
                return Result<LoginDto>.Failure("此信箱尚未註冊");
            }

            if(!_passwordHasher.Verify(request.Password, user.HashedPassword ?? ""))
            {
                return Result<LoginDto>.Failure("密碼檢核錯誤");
            }

            // 撈 Roles
            var roles = await _userRoleRepository.GetRolesByUserId(user.UserId);

            // 撈 Permissions（⭐ 關鍵）
            var permissions = await _rolePermissionRepository.GetRolePermissionByUserId(user.UserId);

            // 組裝 Claims
            var claims = _jwt.BuildClaims(user, roles, permissions);

            // 發 JWT
            var token = _jwt.GenerateToken(claims);

            return Result<LoginDto>.Success(new LoginDto(token));
        }
    }
}