using CSharpFunctionalExtensions;
using Films.Core.FilmManagement.ValueObjects;
using Films.Core.Shared;
using Films.Core.Shared.IDs;

namespace Films.Core.FilmManagement;

/// <summary>
/// Represents a film entity (also it's aggregate root)
/// </summary>
public class Film : Shared.Entity<FilmId>
{
    // It's for EF Core (initialize instance)
    private Film(FilmId id) : base(id) { }

    private Film(
        FilmId id,
        Title title,
        Genre genre,
        Director director,
        ReleaseYear releaseYear,
        Rating rating,
        Description description) : base(id)
    {
        Title = title;
        Genre = genre;
        Director = director;
        ReleaseYear = releaseYear;
        Rating = rating;
        Description = description;
    }

    public Title Title { get; private set; } = default!;
    public Genre Genre { get; private set; } = default!;
    public Director Director { get; private set; } = default!;
    public ReleaseYear ReleaseYear { get; private set; } = default!;
    public Rating Rating { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    
    public static Result<Film, Error> Create(
        FilmId id, 
        Title title,
        Genre genre,
        Director director,
        ReleaseYear releaseYear,
        Rating rating,
        Description description)
    {
        return new Film(
            id, 
            title,
            genre,
            director,
            releaseYear,
            rating,
            description);
    }
}