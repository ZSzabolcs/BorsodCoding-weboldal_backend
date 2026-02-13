using AuthApi.Models;

namespace AuthApi.Services.Interfaces.IAuthService
{
    public interface ITokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> role);
    }
}
