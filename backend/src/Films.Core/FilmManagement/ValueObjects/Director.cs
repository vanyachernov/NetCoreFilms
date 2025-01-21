using CSharpFunctionalExtensions;
using Films.Core.Shared;

namespace Films.Core.FilmManagement.ValueObjects;

/// <summary>
/// Represents a director value-object.
/// </summary>
public record Director
{
    private Director(string value) => Value = value;
    
    public string Value { get; } = default!;

    public static Result<Director, Error> Create(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return Errors.General.ValueIsInvalid("Director's full name is empty or invalid!");
        }

        return new Director(fullName);
    }
}