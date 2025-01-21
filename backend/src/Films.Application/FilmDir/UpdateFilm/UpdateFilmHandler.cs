using CSharpFunctionalExtensions;
using Films.Application.FilmDir.AddFilm;
using Films.Core.FilmManagement;
using Films.Core.FilmManagement.ValueObjects;
using Films.Core.Shared;
using Films.Core.Shared.IDs;

namespace Films.Application.FilmDir.UpdateFilm;

public class UpdateFilmHandler(IFilmsRepository filmsRepository)
{
    public async Task<Result<Guid, Error>> Handle(
        Guid filmId,
        AddFilmRequest film,
        CancellationToken cancellationToken = default)
    {
        var filmExistsResult = await filmsRepository.IsExists(
            filmId,
            cancellationToken);

        if (filmExistsResult.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Film Identifier");
        }

        var filmExists = filmExistsResult.Value;
        
        if (!filmExists)
        {
            return Errors.General.NotFound(filmId);
        }
        
        var filmTitle = Title.Create(film.FullName.Name);
        
        if (filmTitle.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Title");
        }

        var filmDescription = Description.Create(film.FullName.Description);
        
        if (filmDescription.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Description");
        }

        var filmGenre = Genre.Create(film.Genre.Title);
        
        if (filmGenre.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Genre");
        }

        var filmDirector = Director.Create(film.Director.FullName);
        
        if (filmDirector.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Director");
        }
        
        var filmRating = Rating.Create(film.Rating.Points);
        
        if (filmRating.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Rating");
        }

        var filmReleaseYear = ReleaseYear.Create(film.Release.Year);
        
        if (filmReleaseYear.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Release Year");
        }
        
        var filmResult = Film.Create(
            FilmId.Create(filmId), 
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