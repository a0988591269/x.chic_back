using Dapper;
using MediatR;
using MyApp.Application.Commons.Interfaces.Authentication;
using MyApp.Application.Commons.Results;
using MyApp.Domain.Entities;

namespace MyApp.Application.Features.Users.Signup
{
    public class SignupHandler : IRequestHandler<SignupCommand, Result<SignupDto>>
    {
        private readonly IConnectionFactory _factory;
        private readonly IDbContext _db;
        private readonly IPasswordHasher _passwordHasher;

        public SignupHandler(IConnectionFactory factory, IDbContext db, IPasswordHasher passwordHasher) 
        {
            _factory = factory;
            _db = db;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<SignupDto>> Handle(SignupCommand request, CancellationToken cancellationToken)
        {
            using var conn = _factory.GetConnection();

            var check = await conn.ExecuteAsync(@" SELECT COUNT(*) FROM Users WHERE Email = @Email ", new { Email = request.Email });
            if(check > 0)
            {
                return Result<SignupDto>.NotFound("重複註冊！");
            }
            var role = await conn.QueryFirstAsync<Role>(@" SELECT * FROM Roles WHERE Name = 'Customer'");

            //var user = new User { Email = request.Email, Name = request.Name, HashedPassword = PasswordHasher.Hash(request.Passward), Tier = request.Tier };
            var user = new User { Email = request.Email, Name = "Customer", HashedPassword = _passwordHasher.Hash(request.Passward), Tier = 0 };
            var userRole = new UserRole { User = user, Role = role };
            _db.Set<User>().Add(user);
            _db.Set<UserRole>().Add(userRole);
            await _db.SaveChangesAsync();

            return Result<SignupDto>.NotFound("註冊成功！");
        }
    }
}
