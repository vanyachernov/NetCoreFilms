using CSharpFunctionalExtensions;

namespace Films.Core.FilmManagement.ValueObjects;

public record ReleaseYear
{
    private ReleaseYear(int value) => Value = value;
    
    public int Value { get; } = default!;

    public static Result<ReleaseYear> Create(int releaseYear)
    {
        var currentYear = DateTime.UtcNow.Year;
        
        if (releaseYear < 1888 || releaseYear > currentYear)
        {
            return Result.Failure<ReleaseYear>(
                $"Release year {releaseYear} is invalid! Must be between 1888 and {currentYear}.");
        }

        return new ReleaseYear(releaseYear);
    }
}