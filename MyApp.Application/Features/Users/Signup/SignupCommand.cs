using MediatR;
using MyApp.Application.Commons.Results;

namespace MyApp.Application.Features.Users.Signup
{
    //public record SignupCommand(string Email, string Password, string Name = "Customer", byte Tier = 0) : IRequest<Result<SignupDto>> { }
    public record SignupCommand(string Email, string Password) : IRequest<Result<SignupDto>> { }
}
