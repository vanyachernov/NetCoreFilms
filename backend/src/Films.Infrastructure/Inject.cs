using Films.Application.FilmDir;
using Films.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Films.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<FilmDbContext>();
        
        services.AddScoped<IFilmsRepository, FilmsRepository>();

        return services;
    }
}