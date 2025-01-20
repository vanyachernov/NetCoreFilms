using CSharpFunctionalExtensions;
using Films.Core.Shared;

namespace Films.Core.FilmManagement.ValueObjects;

public record Genre
{
    private Genre(string value) => Value = value;
    
    public string Value { get; } = default!;

    public static Result<Genre, Error> Create(string genre)
    {
        if (string.IsNullOrWhiteSpace(genre))
        {
            return Errors.General.ValueIsInvalid("Director's full name is empty or invalid!");
            
        }

        return new Genre(genre);
    }
}