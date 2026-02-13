using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Users.GetAllUsers
{
    public record GetAllUsersDto(long Id, string Name, string Email, bool Status, List<dynamic> Permissions) { }
}
