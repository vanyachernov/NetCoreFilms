using CSharpFunctionalExtensions;

namespace Films.Core.FilmManagement.ValueObjects;

public record Description
{
    private Description(string value) => Value = value;

    public string Value { get; } = default!;

    public static Result<Description> Create(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            Result.Failure<Description>("Description is invalid!");
        }
        
        return new Description(description);
    }
}