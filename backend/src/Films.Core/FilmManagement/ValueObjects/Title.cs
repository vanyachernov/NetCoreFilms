using CSharpFunctionalExtensions;
using Films.Core.Shared;

namespace Films.Core.FilmManagement.ValueObjects;

public record Title
{
    private Title(string value) => Value = value;
    
    public string Value { get; } = default!;

    public static Result<Title, Error> Create(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Errors.General.ValueIsInvalid("Title is empty or invalid!");
        }

        return new Title(title);
    }
}