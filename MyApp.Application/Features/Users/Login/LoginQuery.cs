using MediatR;
using MyApp.Application.Commons.Results;

namespace MyApp.Application.Features.Users.Login
{
    public record LoginQuery(string Email, string Password) : IRequest<Result<LoginDto>> { };
}
