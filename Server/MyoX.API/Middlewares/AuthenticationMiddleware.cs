using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MyoX.API.Middlewares
{
    public static class AuthenticationMiddleware
    {
        public static IServiceCollection AddAuthenticationMiddleware(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddAuthorization()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = config["Authentication:issuer"],
                        ValidAudience = config["Authentication:audience"],
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["Authentication:secret"] ?? throw new ArgumentNullException("Authentication:secret")))
                    };
                });
            return services;
        }
    }
}
