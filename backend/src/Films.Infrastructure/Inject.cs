using Microsoft.Extensions.DependencyInjection;

namespace Films.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<FilmDbContext>();

        return services;
    }
}