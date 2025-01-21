using CSharpFunctionalExtensions;
using Films.Application.DTOs;
using Films.Application.FilmDir.GetFilms;
using Films.Core.Shared;

namespace Films.Application.FilmDir.GetFilm;

public class GetFilmHandler(IFilmsRepository filmsRepository)
{
    public async Task<Result<GetFilmsResponse, Error>> Handle(
        Guid filmId,
        CancellationToken cancellationToken = default)
    {
        var filmResult = await filmsRepository.GetById(
            filmId,
            cancellationToken);
        
        if (filmResult.IsFailure)
        {
            return Errors.General.NotFound();
        }
        
        var film = filmResult.Value;
        
        var filmDto = new GetFilmsResponse
        {
            Id = film.Id,
            FullName = film.FullName,
            Genre = film.Genre,
            Director = film.Director,
            Rating = film.Rating,
            Release = film.Release
        };

        return filmDto;
    }
}