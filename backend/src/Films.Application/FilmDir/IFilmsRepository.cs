using CSharpFunctionalExtensions;
using Films.Core.FilmManagement;

namespace Films.Application.FilmDir;

public interface IFilmsRepository
{
    Task<Result<IEnumerable<Film>>> Get(CancellationToken cancellationToken = default);

    Task<Result<Film>> GetById(CancellationToken cancellationToken = default);
    
    Task<Result<Guid>> Create(
        Film film,
        CancellationToken cancellationToken = default);

    Task<Result<Film>> Update(
        Film film,
        CancellationToken cancellationToken = default);
    
    Task<Result<Guid>> Delete(
        Guid filmId,
        CancellationToken cancellationToken = default);
}