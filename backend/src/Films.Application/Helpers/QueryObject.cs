namespace Films.Application.Helpers;

public class QueryObject
{
    public string? Symbol { get; set; } = null;
    public string? Director { get; set; } = null;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}