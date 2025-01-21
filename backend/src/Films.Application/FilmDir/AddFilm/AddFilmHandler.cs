using CSharpFunctionalExtensions;
using Films.Core.FilmManagement;
using Films.Core.FilmManagement.ValueObjects;
using Films.Core.Shared;
using Films.Core.Shared.IDs;

namespace Films.Application.FilmDir.AddFilm;

public class AddFilmHandler(IFilmsRepository filmsRepository)
{
    public async Task<Result<Guid, Error>> Handle(
        AddFilmRequest request,
        CancellationToken cancellationToken = default)
    {
        var filmTitle = Title.Create(request.FullName.Name);
        
        if (filmTitle.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Title");
        }

        var filmGenre = Genre.Create(request.Genre.Title);
        
        if (filmGenre.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Genre");
        }

        var filmDirector = Director.Create(request.Director.FullName);
        
        if (filmDirector.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Director");
        }

        var filmReleaseYear = ReleaseYear.Create(request.Release.Year);
        
        if (filmReleaseYear.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Release Year");
        }
        
        var filmRating = Rating.Create(request.Rating.Points);
        
        if (filmRating.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Rating");
        }
        
        var filmDescription = Description.Create(request.FullName.Description);
        
        if (filmDescription.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Description");
        }

        var film = Film.Create(
            FilmId.NewId,
            filmTitle.Value,
            filmGenre.Value,
            filmDirector.Value,
            filmReleaseYear.Value,
            filmRating.Value,
            filmDescription.Value);

        if (film.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Film");
        }

        var creatingFilmHandler = await filmsRepository.Create(
            film.Value, 
            cancellationToken);

        if (creatingFilmHandler.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Film");
        }

        return creatingFilmHandler.Value;
    }
}