using MediatR;
using MyApp.Application.Commons.Results;

namespace MyApp.Application.Features.Users.Signup
{
    public record SignupCommand(string Email, string Password) : IRequest<Result<SignupDto>> { }
}
