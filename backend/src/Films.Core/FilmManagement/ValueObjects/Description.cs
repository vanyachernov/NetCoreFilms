using CSharpFunctionalExtensions;
using Films.Core.Shared;

namespace Films.Core.FilmManagement.ValueObjects;

public record Description
{
    private Description(string value) => Value = value;

    public string Value { get; } = default!;

    public static Result<Description, Error> Create(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return Errors.General.ValueIsInvalid("Description is invalid");
        }
        
        return new Description(description);
    }
}