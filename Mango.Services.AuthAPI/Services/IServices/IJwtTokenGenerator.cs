using Mango.Services.AuthAPI.Models;

namespace Mango.Services.AuthAPI.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUser applicationUser);
    }
}