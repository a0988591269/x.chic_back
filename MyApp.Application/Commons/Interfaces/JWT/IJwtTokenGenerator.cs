using System.Security.Claims;

namespace MyApp.Application.Commons.Interfaces.JWT
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
