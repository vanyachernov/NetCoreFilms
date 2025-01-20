using CSharpFunctionalExtensions;
using Films.Core.Shared;

namespace Films.Core.FilmManagement.ValueObjects;

public record Rating
{
    private Rating(int value) => Value = value;
    
    public int Value { get; } = default!;

    public static Result<Rating, Error> Create(int rating)
    {
        if (rating < 0 || rating > 10)
        {
            return Errors.General.ValueIsInvalid("Rating is invalid!");
        }

        return new Rating(rating);
    }
}