using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProductsAPI.Extensions
{
    public static class Startup
    {
        public static WebApplicationBuilder AddAppAuthentication (this WebApplicationBuilder builder)
        {
            return builder;
        }
    }
}
