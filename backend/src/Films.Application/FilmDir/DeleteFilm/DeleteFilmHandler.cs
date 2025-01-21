using CSharpFunctionalExtensions;
using Films.Core.Shared;

namespace Films.Application.FilmDir.DeleteFilm;

public class DeleteFilmHandler(IFilmsRepository filmsRepository)
{
    public async Task<Result<Guid, Error>> Handle(
        Guid filmId,
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

        var deleteFilmResult = await filmsRepository.Delete(
            filmId,
            cancellationToken);

        if (deleteFilmResult.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Delete film");
        }

        return deleteFilmResult.Value;
    }
}