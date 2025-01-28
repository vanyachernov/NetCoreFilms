using CSharpFunctionalExtensions;
using Films.Application.FilmDir.GetFilms;
using Films.Application.Helpers;
using Films.Core.FilmManagement;
using Films.Core.Shared;

namespace Films.Application.FilmDir;

/// <summary>
/// Represents a film repository.
/// </summary>
public interface IFilmsRepository
{
    /// <summary>
    /// Gets a list of films.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{IEnumerable{GetFilmsReponse}}"/>.</returns>
    Task<Result<IEnumerable<GetFilmsResponse>, Error>> Get(
        QueryObject query,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a film.
    /// </summary>
    /// <param name="filmId">A film identifier.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{GetFilmsReponse}"/>.</returns>
    Task<Result<GetFilmsResponse, Error>> GetById(
        Guid filmId,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a new film.
    /// </summary>
    /// <param name="film">A <see cref="Film"/>.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{Guid}"/>.</returns>
    Task<Result<Guid, Error>> Create(
        Film film,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing film.
    /// </summary>
    /// <param name="film">A <see cref="Film"/>.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{Film}"/>.</returns>
    Task<Result<Film, Error>> Update(
        Film film,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a new film.
    /// </summary>
    /// <param name="filmId">A film identifier.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{Guid}"/>.</returns>
    Task<Result<Guid, Error>> Delete(
        Guid filmId,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Checks if a film exists.
    /// </summary>
    /// <param name="filmId">A film identifier.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{boolean}"/>.</returns>
    Task<Result<bool, Error>> IsExists(
        Guid filmId, 
        CancellationToken cancellationToken = default);
}