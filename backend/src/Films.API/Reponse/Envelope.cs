namespace Films.API.Reponse;

public class Envelope
{
    private Envelope(
        object? result, 
        IEnumerable<ResponseError> errors)
    {
        Result = result;
        Errors = errors.ToList();
        TimeGenerated = DateTime.Now;
    }

    public object? Result { get; }
    public List<ResponseError> Errors { get; }
    public DateTime TimeGenerated { get; }

    public static Envelope Ok(object? result = null) =>
        new(result, []);
    
    public static Envelope Error(IEnumerable<ResponseError> errors) =>
        new(null, errors);
}