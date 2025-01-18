using CSharpFunctionalExtensions;

namespace Films.Core.FilmManagement.ValueObjects;

public record Genre
{
    private Genre(string value) => Value = value;
    
    public string Value { get; } = default!;

    public static Result<Genre> Create(string genre)
    {
        if (string.IsNullOrWhiteSpace(genre))
        {
            return Result.Failure<Genre>("Genre is empty or invalid!");
        }

        return new Genre(genre);
    }
}