using CSharpFunctionalExtensions;

namespace Films.Core.FilmManagement.ValueObjects;

public record Title
{
    private Title(string value) => Value = value;
    
    public string Value { get; } = default!;

    public static Result<Title> Create(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure<Title>("Title is empty or invalid!");
        }

        return new Title(title);
    }
}