using MediatR;
using MyApp.Application.Commons.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Users.Signup
{
    //public record SignupCommand(string Email, string Passward, string Name = "Customer", byte Tier = 0) : IRequest<Result<SignupDto>> {}
    public record SignupCommand(string Email, string Passward) : IRequest<Result<SignupDto>> { }
}
