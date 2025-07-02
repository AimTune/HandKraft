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

        List<TypeInfo> allTypes = assemblies.SelectMany(a => a.DefinedTypes).ToList();

        foreach (TypeInfo type in allTypes)
        {
            // Only consider classes that are not abstract
            if (type.IsClass && !type.IsAbstract)
            {
                // All implemented interfaces
                Type[] interfaces = type.GetInterfaces();

                // Register ICommandHandler<T>
                IEnumerable<Type> commandHandlerInterfaces = interfaces
                    .Where(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)));

                foreach (Type iface in commandHandlerInterfaces)
                {
                    services.AddTransient(iface, type.AsType());
                }

                // Register IQQueryHandler<T>
                IEnumerable<Type> qqueryHandlerInterfaces = interfaces
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQQueryHandler<,>));

                foreach (Type iface in qqueryHandlerInterfaces)
                {
                    services.AddTransient(iface, type.AsType());
                }
            }
        }

        return services;
    }
}
