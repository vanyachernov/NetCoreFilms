using CSharpFunctionalExtensions;
using Films.Core.FilmManagement;
using Films.Core.Shared;

namespace Films.Application.FilmDir;

public interface IFilmsRepository
{
    Task<Result<IEnumerable<Film>, Error>> Get(CancellationToken cancellationToken = default);

    Task<Result<Film, Error>> GetById(
        Guid filmId,
        CancellationToken cancellationToken = default);
    
    Task<Result<Guid, Error>> Create(
        Film film,
        CancellationToken cancellationToken = default);

    Task<Result<Film, Error>> Update(
        Film film,
        CancellationToken cancellationToken = default);
    
    Task<Result<Guid, Error>> Delete(
        Guid filmId,
        CancellationToken cancellationToken = default);
    
    Task<Result<bool, Error>> IsExists(
        Guid templateId, 
        CancellationToken cancellationToken = default);
}