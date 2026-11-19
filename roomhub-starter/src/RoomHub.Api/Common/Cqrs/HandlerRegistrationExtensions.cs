using System.Reflection;

namespace RoomHub.Api.Common.Cqrs;

public static class HandlerRegistrationExtensions
{
    private static readonly Type[] HandlerContracts = [typeof(ICommandHandler<,>), typeof(IQueryHandler<,>)];

    public static IServiceCollection AddHandlers(this IServiceCollection services, Assembly assembly)
    {
        var implementations = assembly.GetTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false });

        foreach (var implementation in implementations)
        {
            var contracts = implementation.GetInterfaces()
                .Where(contract => contract.IsGenericType
                    && HandlerContracts.Contains(contract.GetGenericTypeDefinition()));

            foreach (var contract in contracts)
            {
                services.AddScoped(contract, implementation);
            }
        }

        return services;
    }
}
