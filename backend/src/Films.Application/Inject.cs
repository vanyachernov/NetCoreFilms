using Films.Application.FilmDir.AddFilm;
using Films.Application.FilmDir.DeleteFilm;
using Films.Application.FilmDir.GetFilm;
using Films.Application.FilmDir.GetFilms;
using Films.Application.FilmDir.UpdateFilm;
using Films.Application.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Films.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AddFilmHandler>();
        
        services.AddScoped<GetFilmHandler>();
        
        services.AddScoped<GetFilmsHandler>();
        
        services.AddScoped<UpdateFilmHandler>();
        
        services.AddScoped<DeleteFilmHandler>();
        
        services.AddScoped<QueryObject>();
        
        return services;
    }
}