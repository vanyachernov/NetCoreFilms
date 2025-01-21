using Films.API.Reponse;
using Films.Core.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Films.API.Extensions;

public static class ResponseExtenstions
{
    public static ActionResult ToResponse(this Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        var responseError = new ResponseError(
            error.Code, 
            error.Message, 
            null);

        var envelope = Envelope.Error([responseError]);
            
        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }
}