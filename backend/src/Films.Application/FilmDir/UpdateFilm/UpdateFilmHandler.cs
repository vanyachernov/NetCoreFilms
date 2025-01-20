using CSharpFunctionalExtensions;
using Films.Core.FilmManagement;
using Films.Core.FilmManagement.ValueObjects;
using Films.Core.Shared;

namespace Films.Application.FilmDir.UpdateFilm;

public class UpdateFilmHandler(IFilmsRepository filmsRepository)
{
    public async Task<Result<Guid, Error>> Handle(
        Film film,
        CancellationToken cancellationToken = default)
    {
        var filmExistsResult = await filmsRepository.IsExists(
            film.Id,
            cancellationToken);

        if (filmExistsResult.IsFailure)
        {
            Errors.General.ValueIsInvalid("Film Identifier");
        }

        var filmExists = filmExistsResult.Value;
        
        if (!filmExists)
        {
            Errors.General.NotFound(film.Id);
        }
        
        var filmTitle = Title.Create(film.Title.Value);
        
        if (filmTitle.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Title");
        }

        var filmDescription = Description.Create(film.Description.Value);
        
        if (filmDescription.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Description");
        }

        var filmGenre = Genre.Create(film.Genre.Value);
        
        if (filmGenre.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Genre");
        }

        var filmDirector = Director.Create(film.Director.Value);
        
        if (filmDirector.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Director");
        }
        
        var filmRating = Rating.Create(film.Rating.Value);
        
        if (filmRating.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Rating");
        }

        var filmReleaseYear = ReleaseYear.Create(film.ReleaseYear.Value);
        
        if (filmReleaseYear.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Release Year");
        }
        
        var filmResult = Film.Create(
            film.Id,
            filmTitle.Value,
            filmGenre.Value,
            filmDirector.Value,
            filmReleaseYear.Value,
            filmRating.Value,
            filmDescription.Value);

        if (filmResult.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Film");
        }

        var updatedFilm = filmResult.Value;

        var updatingFilmResult = await filmsRepository.Update(
            updatedFilm, 
            cancellationToken);

        if (updatingFilmResult.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Updating film");
        }

        return updatedFilm.Id.Value;
    }
}