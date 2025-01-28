namespace Films.Application.Helpers;

public class QueryObject
{
    public string? Title { get; set; } = null;
    public string? Director { get; set; } = null;
    public bool IsRatingDescending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}