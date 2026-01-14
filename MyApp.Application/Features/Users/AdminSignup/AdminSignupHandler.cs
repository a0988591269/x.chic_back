using MediatR;
using MyApp.Application.Commons.Interfaces.Authentication;
using MyApp.Application.Commons.Results;
using MyApp.Application.Features.Users.Signup;
using MyApp.Domain.Constants;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Users.AdminSignup
{
    public class AdminSignupHandler : IRequestHandler<AdminSignupCommand, Result<AdminSigupDto>>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;

        public AdminSignupHandler(
            IPasswordHasher passwordHasher,
            IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public async Task<Result<AdminSigupDto>> Handle(AdminSignupCommand request, CancellationToken cancellationToken)
        {
            // 檢核是否重複註冊
            var isUnique = await _userRepository.IsEmailUniqueAsync(request.Email, cancellationToken);
            if (!isUnique)
            {
                return Result<AdminSigupDto>.Conflict("重複註冊！");
            }

            string suffix = Guid.NewGuid().ToString().Split('-')[0];
            // 建立基本 User
            var user = User.Create(request.Email, _passwordHasher.Hash(request.Password), $"x.chic.{suffix}");

            // 建立基本 UserRole
            user.AddRole(RoleId.Member);

            await _userRepository.AddAsync(user, cancellationToken);

            return Result<AdminSigupDto>.Created(new AdminSigupDto());
        }
    }
}
