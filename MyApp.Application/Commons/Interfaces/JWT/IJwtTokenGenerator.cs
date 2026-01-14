using MyApp.Domain.Entities;
using System.Security.Claims;

namespace MyApp.Application.Commons.Interfaces.JWT
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IEnumerable<Claim> claims);

        List<Claim> BuildClaims(User user, IEnumerable<string> roles, IEnumerable<string> permissions);
    }
}
