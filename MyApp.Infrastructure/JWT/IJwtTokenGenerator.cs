using System.Security.Claims;

namespace MyApp.Infrastructure.JWT
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
