using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces;
using Infrastructure;
using Application.Interfaces;

namespace Infrastructure.DI;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IAnimalRepository, InMemoryAnimalRepository>();
        services.AddSingleton<IEnclosureRepository, InMemoryEnclosureRepository>();
        services.AddSingleton<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>();

        return services;
    }
}