using CSharpFunctionalExtensions;
using Films.Core.Shared;

namespace Films.Core.FilmManagement.ValueObjects;

/// <summary>
/// Represents a rating value-object.
/// </summary>
public record Rating
{
    private Rating(int value) => Value = value;
    
    public int Value { get; } = default!;

    public static Result<Rating, Error> Create(int rating)
    {
        if (rating < 1 || rating > 10)
        {
            return Errors.General.ValueIsInvalid("Rating is invalid!");
        }

        return new Rating(rating);
    }
}