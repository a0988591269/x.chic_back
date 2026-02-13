using MediatR;
using MyApp.Application.Commons.Results;
using MyApp.Application.Features.Products.Queries.GetProductDetail;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Users.GetAllUsers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Result<List<GetAllUsersDto>>>
    {
        private readonly IUserRepository _userRepository;

        public async Task<Result<List<GetAllUsersDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var data = await _userRepository.GetAllUsers();

            if(data == null)
            {
                return Result<List<GetAllUsersDto>>.NotFound();
            }

            var users = data.Select(u => new GetAllUsersDto(u.UserId, u.Email, u.Name, u.Status)).ToList();

            throw new NotImplementedException();
        }
    }
}
