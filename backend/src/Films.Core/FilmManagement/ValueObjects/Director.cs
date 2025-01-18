using CSharpFunctionalExtensions;

namespace Films.Core.FilmManagement.ValueObjects;

public record Director
{
    private Director(string value) => Value = value;
    
    public string Value { get; } = default!;

    public static Result<Director> Create(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return Result.Failure<Director>("Director's full name is empty or invalid!");
        }

        return new Director(fullName);
    }
}