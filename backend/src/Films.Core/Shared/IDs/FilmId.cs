namespace Films.Core.Shared.IDs;

public record FilmId
{
    public Guid Value { get; }
    
    private FilmId(Guid value) => Value = value;
    
    public static FilmId Create(Guid id) => new(id);

    public static implicit operator Guid(FilmId id) => id.Value;
}