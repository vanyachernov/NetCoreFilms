using CSharpFunctionalExtensions;
using Films.Core.FilmManagement;
using Films.Core.FilmManagement.ValueObjects;
using Films.Core.Shared.IDs;

namespace Films.Application.FilmDir.AddFilm;

public class AddFilmHandler(IFilmsRepository filmsRepository)
{
    public async Task<Result<Guid>> Handle(
        Guid filmId,
        AddFilmRequest request,
        CancellationToken cancellationToken = default)
    {
        var filmTitle = Title.Create(request.FullName.Name);
        
        if (filmTitle.IsFailure)
        {
            return Result.Failure<Guid>("Film Title is invalid!");
        }

        var filmGenre = Genre.Create(request.Genre.Title);
        
        if (filmGenre.IsFailure)
        {
            return Result.Failure<Guid>("Film Genre is invalid!");
        }

        var filmDirector = Director.Create(request.Director.FullName);
        
        if (filmDirector.IsFailure)
        {
            return Result.Failure<Guid>("Film Director is invalid!");
        }

        var filmReleaseYear = ReleaseYear.Create(request.Release.Year);
        
        if (filmReleaseYear.IsFailure)
        {
            return Result.Failure<Guid>("Film Release Year is invalid!");
        }
        
        var filmRating = Rating.Create(request.Rating.Points);
        
        if (filmRating.IsFailure)
        {
            return Result.Failure<Guid>("Film Rating is invalid!");
        }
        
        var filmDescription = Description.Create(request.FullName.Description);
        
        if (filmDescription.IsFailure)
        {
            return Result.Failure<Guid>("Film Description is invalid!");
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
            return Result.Failure<Guid>("Error creating film!");
        }

        var creatingFilmHandler = await filmsRepository.Create(
            film.Value, 
            cancellationToken);

        if (creatingFilmHandler.IsFailure)
        {
            return Result.Failure<Guid>("Error creating film!");
        }

        return creatingFilmHandler.Value;
    }
}