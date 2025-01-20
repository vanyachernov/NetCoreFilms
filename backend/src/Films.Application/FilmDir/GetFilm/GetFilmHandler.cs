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
            FullName = new FullNameDto(
                film.Title.Value,
                film.Description.Value),
            Genre = new GenreDto(film.Genre.Value),
            Director = new DirectorDto(film.Director.Value),
            Rating = new RatingDto(film.Rating.Value),
            Release = new ReleaseYearDto(film.ReleaseYear.Value)
        };

        return filmDto;
    }
}