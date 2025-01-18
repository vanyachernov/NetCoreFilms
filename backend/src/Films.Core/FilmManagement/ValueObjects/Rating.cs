using CSharpFunctionalExtensions;

namespace Films.Core.FilmManagement.ValueObjects;

public record Rating
{
    private Rating(int value) => Value = value;
    
    public int Value { get; } = default!;

    public static Result<Rating> Create(int rating)
    {
        if (rating < 0 || rating > 10)
        {
            return Result.Failure<Rating>("Rating is invalid!");
        }

        return new Rating(rating);
    }
}