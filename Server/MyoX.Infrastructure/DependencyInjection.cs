using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyoX.Application.Abstraction;
using MyoX.Domain.Interfaces;
using MyoX.Infrastructure.Database;
using MyoX.Infrastructure.Repository;
using MyoX.Infrastructure.Services;

namespace MyoX.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddScoped<IUserRepository, UserRepository>()
                .AddSingleton<ITokenService, TokenService>()
                .AddDbContext<AppDbContext>(options =>
                {
                    string connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                    options.UseSqlServer(connectionString);
                });
            return services;
        }
    }
}
