namespace Films.API.Reponse;

public record ResponseError(
    string? ErrorCode, 
    string? ErrorMessage, 
    string? InvalidField);