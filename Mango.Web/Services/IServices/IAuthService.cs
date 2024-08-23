using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDto);
        Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registrationRequestDto);
        Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDto);
    }
}
