using MediatR;
using MyApp.Application.Commons.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Users.GetAllUsers
{
    public record GetAllUsersQuery : IRequest<Result<List<GetAllUsersDto>>>
    {
    }
}
