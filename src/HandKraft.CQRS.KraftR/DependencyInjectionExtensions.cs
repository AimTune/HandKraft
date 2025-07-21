using HandKraft.Cqrs.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HandKraft.CQRS.KraftR;

public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Registers KraftR mediator and handlers from the specified assemblies.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="assemblies">Assemblies to scan for handlers.</param>
    /// <returns>IServiceCollection for chaining.</returns>
    public static IServiceCollection AddKraftR(this IServiceCollection services, params Assembly[] assemblies)
    {
        // KraftR mediator
        services.AddSingleton<IKraftR, KraftR>();

        var allTypes = assemblies.SelectMany(a => a.DefinedTypes).ToList();

        foreach (var type in allTypes)
        {
            if (type.IsClass && !type.IsAbstract)
            {
                var interfaces = type.GetInterfaces();

                var commandHandlerInterfaces = interfaces
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                    .Distinct();

                foreach (var iface in commandHandlerInterfaces)
                {
                    services.AddTransient(iface, type.AsType());
                }

                var queryHandlerInterfaces = interfaces
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQQueryHandler<,>))
                    .Distinct();

                foreach (var iface in queryHandlerInterfaces)
                {
                    services.AddTransient(iface, type.AsType());
                }
            }
        }

        return services;
    }
}
