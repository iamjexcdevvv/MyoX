using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using MyoX.Application.Abstraction.Command;
using MyoX.Application.Abstraction.Query;
using MyoX.Application.Features.Authentication.Register;

namespace MyoX.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.Scan(scan => scan.FromAssembliesOf(typeof(DependencyInjection))
                    .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            services
                .AddValidatorsFromAssemblyContaining<RegisterCommandValidator>()
                .AddMapster();

            return services;
        }
    }
}
