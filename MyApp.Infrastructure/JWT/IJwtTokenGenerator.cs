using System.Security.Claims;

namespace MyApp.Infrastructure.JWT
{
    public interface IJwtTokenGenerator
    {
        string Generate(IEnumerable<Claim> claims);
    }
}
