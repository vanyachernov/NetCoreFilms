using CSharpFunctionalExtensions;
using Films.Application.DTOs;
using Films.Application.FilmDir;
using Films.Application.FilmDir.GetFilms;
using Films.Application.Helpers;
using Films.Core.FilmManagement;
using Films.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace Films.Infrastructure.Repositories;

public class FilmsRepository(FilmDbContext context) : IFilmsRepository
{
    public async Task<Result<IEnumerable<GetFilmsResponse>, Error>> Get(
        QueryObject query,
        CancellationToken cancellationToken = default)
    {
        var films = context.Films
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Director))
        {
            var directorFilter = query.Director.ToLower();
            
            films = films.Where(f => f.Director.Value
                .ToLower()
                .Contains(directorFilter));
        }
        
        if (!string.IsNullOrWhiteSpace(query.Title))
        {
            var titleFilter = query.Title.ToLower();
            
            films = films
                .Where(f => f.Title.Value
                    .ToLower()
                    .Contains(titleFilter));
        }

        films = query.IsRatingDescending
            ? films.OrderByDescending(f => f.Rating.Value)
            : films.OrderBy(f => f.Rating.Value);
        
        var response = films.Select(film => new GetFilmsResponse
        {
            Id = film.Id.Value,
            FullName = new FullNameDto(
                film.Title.Value,
                film.Description.Value
            ),
            Genre = new GenreDto(film.Genre.Value),
            Director = new DirectorDto(film.Director.Value),
            Rating = new RatingDto(film.Rating.Value),
            Release = new ReleaseYearDto(film.ReleaseYear.Value)
        });

        var paginationResult = (query.PageNumber - 1) * query.PageSize;
        
        return await response
            .Skip(paginationResult)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Result<GetFilmsResponse, Error>> GetById(
        Guid filmId, 
        CancellationToken cancellationToken = default)
    {
        var film = await context.Films
            .FirstOrDefaultAsync(
                f => f.Id == filmId, 
                cancellationToken);

        if (film == null)
        {
            return Errors.General.NotFound(filmId);
        }
        
        var response = new GetFilmsResponse
        {
            Id = film.Id.Value,
            FullName = new FullNameDto(
                film.Title.Value,
                film.Description.Value
            ),
            Genre = new GenreDto(film.Genre.Value),
            Director = new DirectorDto(film.Director.Value),
            Rating = new RatingDto(film.Rating.Value),
            Release = new ReleaseYearDto(film.ReleaseYear.Value)
        };

        return response;
    }

    public async Task<Result<Guid, Error>> Create(
        Film film, 
        CancellationToken cancellationToken = default)
    {
        await context.Films
            .AddAsync(
                film, 
                cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
        
        return film.Id.Value;
    }

    public async Task<Result<Film, Error>> Update(
        Film film, 
        CancellationToken cancellationToken = default)
    {
        await context.Films
            .Where(f => f.Id == film.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(f => f.Title, film.Title)
                .SetProperty(f => f.Description, film.Description)
                .SetProperty(f => f.Genre, film.Genre)
                .SetProperty(f => f.Director, film.Director)
                .SetProperty(f => f.Rating, film.Rating)
                .SetProperty(f => f.ReleaseYear, film.ReleaseYear), 
                cancellationToken);

        return film;
    }

    public async Task<Result<Guid, Error>> Delete(
        Guid filmId, 
        CancellationToken cancellationToken = default)
    {
        await context.Films
            .Where(f => f.Id == filmId)
            .ExecuteDeleteAsync(cancellationToken);

        return filmId;
    }

    public async Task<Result<bool, Error>> IsExists(
        Guid filmId, 
        CancellationToken cancellationToken = default)
    {
        var film = await context.Films
            .FirstOrDefaultAsync(
                f => f.Id == filmId, 
                cancellationToken);

        var isFilmExists = film != null;

        return isFilmExists;
    }
}