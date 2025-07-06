namespace MyoX.API.Middlewares
{
    public static class CorsMiddleware
    {
        public static IServiceCollection AddCorsMiddleware(this IServiceCollection services, IConfiguration config, string policyName)
        {
            services.AddCors(options =>
            {
                string[] allowedOrigins = config.GetSection("AllowedOrigins").Get<string[]>() ?? 
                throw new ArgumentNullException("'AllowedOrigins' configuration is missing or empty.");

                options.AddPolicy(policyName, cfg =>
                {
                    cfg
                    .WithOrigins(allowedOrigins)
                    .AllowCredentials()
                    .WithHeaders("Content-Type")
                    .WithMethods("POST", "GET")
                    .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
                });
            });
            return services;
        }
    }
}
