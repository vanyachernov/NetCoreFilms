using Films.API.Controllers.Shared;
using Films.API.Extensions;
using Films.Application.FilmDir.AddFilm;
using Films.Application.FilmDir.DeleteFilm;
using Films.Application.FilmDir.GetFilm;
using Films.Application.FilmDir.GetFilms;
using Films.Application.FilmDir.UpdateFilm;
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
        var newFilmResult = await handler.Handle(
            request,
            cancellationToken);
        
        return newFilmResult.IsFailure 
            ? newFilmResult.Error.ToResponse() 
            : Ok(newFilmResult.Value);
    }
    
    [HttpPut]
    [Route("{filmId:guid}")]
    public async Task<ActionResult<Guid>> Update(
        [FromRoute] Guid filmId,
        [FromBody] AddFilmRequest request,
        [FromServices] UpdateFilmHandler handler,
        CancellationToken cancellationToken = default)
    {
        var filmResult = await handler.Handle(
            filmId,
            request,
            cancellationToken);
        
        return filmResult.IsFailure 
            ? filmResult.Error.ToResponse() 
            : Ok(filmResult.Value);
    }

    [HttpDelete]
    [Route("{filmId:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid filmId,
        [FromServices] DeleteFilmHandler handler,
        CancellationToken cancellationToken = default)
    {
        var filmResult = await handler.Handle(
            filmId,
            cancellationToken);

        return filmResult.IsFailure
            ? filmResult.Error.ToResponse()
            : Ok(filmResult.Value);
    }
}