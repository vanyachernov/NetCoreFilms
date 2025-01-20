using Films.API.Controllers.Shared;
using Films.API.Extensions;
using Films.Application.FilmDir.AddFilm;
using Films.Application.FilmDir.GetFilm;
using Films.Application.FilmDir.GetFilms;
using Microsoft.AspNetCore.Mvc;

namespace Films.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmsController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult<GetFilmsWrapperResponse>> Get(
        [FromServices] GetFilmsHandler handler,
        CancellationToken cancellationToken = default)
    {
        var filmsResult = await handler.Handle(cancellationToken);

        return Ok(filmsResult.Value);
    }
    
    [HttpGet]
    [Route("{filmId:guid}")]
    public async Task<ActionResult<GetFilmsResponse>> GetById(
        [FromRoute] Guid filmId,
        [FromServices] GetFilmHandler handler,
        CancellationToken cancellationToken = default)
    {
        var filmResult = await handler.Handle(
            filmId,
            cancellationToken);

        return filmResult.IsFailure 
            ? filmResult.Error.ToResponse() 
            : Ok(filmResult.Value);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] AddFilmRequest request,
        [FromServices] AddFilmHandler handler,
        CancellationToken cancellationToken = default)
    {
        var filmsResult = await handler.Handle(
            request,
            cancellationToken);
        
        return filmsResult.IsFailure 
            ? filmsResult.Error.ToResponse() 
            : Ok(filmsResult.Value);
    }
}