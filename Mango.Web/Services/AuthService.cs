using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Mango.Web.Utility;

namespace Mango.Web.Services
{
    public class AuthService:IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }


        public async Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticData.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticData.AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticData.ApiType.POST,
                Data = loginRequestDto,
                Url = StaticData.AuthAPIBase + "/api/auth/login"
            });
        }

        public async Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticData.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticData.AuthAPIBase + "/api/auth/register"
            });
        }

    }
}
