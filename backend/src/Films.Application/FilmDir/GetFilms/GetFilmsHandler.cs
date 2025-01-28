using CSharpFunctionalExtensions;
using Films.Application.DTOs;
using Films.Application.Helpers;
using Films.Core.Shared;

namespace Films.Application.FilmDir.GetFilms;

public class GetFilmsHandler(IFilmsRepository filmsRepository)
{
    public async Task<Result<GetFilmsWrapperResponse, Error>> Handle(
        QueryObject query,
        CancellationToken cancellationToken = default)
    {
        var filmsResult = await filmsRepository.Get(
            query,
            cancellationToken);

        if (filmsResult.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Films");
        }

        var response = filmsResult.Value
            .Select(film => new GetFilmsResponse
            {
                Id = film.Id,
                FullName = film.FullName,
                Genre = film.Genre,
                Director = film.Director,
                Rating = film.Rating,
                Release = film.Release
            });

        return new GetFilmsWrapperResponse
        {
            Films = response.ToList()
        };
    }
}