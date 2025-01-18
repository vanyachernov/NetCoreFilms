using CSharpFunctionalExtensions;

namespace Films.Core.FilmManagement.ValueObjects;

public record ReleaseYear
{
    private ReleaseYear(int value) => Value = value;
    
    public int Value { get; } = default!;

    public static Result<ReleaseYear> Create(int releaseYear)
    {
        if (releaseYear <= 0)
        {
            return Result.Failure<ReleaseYear>("Release year is invalid!");
        }

        return new ReleaseYear(releaseYear);
    }
}