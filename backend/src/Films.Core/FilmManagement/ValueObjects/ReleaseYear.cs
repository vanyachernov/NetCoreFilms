using CSharpFunctionalExtensions;
using Films.Core.Shared;

namespace Films.Core.FilmManagement.ValueObjects;

/// <summary>
/// Represents a release year value-object.
/// </summary>
public record ReleaseYear
{
    private ReleaseYear(int value) => Value = value;
    
    public int Value { get; } = default!;

    public static Result<ReleaseYear, Error> Create(int releaseYear)
    {
        var currentYear = DateTime.UtcNow.Year;
        
        if (releaseYear < 1888 || releaseYear > currentYear)
        {
            return Errors.General.ValueIsInvalid(
                $"Release year {releaseYear} is invalid! Must be between 1888 and {currentYear}.");
        }

        return new ReleaseYear(releaseYear);
    }
}