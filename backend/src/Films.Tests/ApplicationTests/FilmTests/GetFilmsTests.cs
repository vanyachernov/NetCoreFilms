using Films.Application.DTOs;
using Films.Application.FilmDir;
using Films.Application.FilmDir.GetFilms;
using Films.Application.Helpers;
using Films.Core.FilmManagement;
using Films.Core.FilmManagement.ValueObjects;
using Films.Core.Shared;
using Films.Core.Shared.IDs;
using Moq;

namespace Films.Tests.ApplicationTests.FilmTests;

/// <summary>
/// Represents a tests for GetFilmsHandler
/// </summary>
public class GetFilmsTests
{
    private readonly Mock<IFilmsRepository> _filmsRepositoryMock = new();
 
    private Film CreateFilm()
    {
        var titleResult = Title.Create("Inception");
        var genreResult = Genre.Create("Science Fiction");
        var directorResult = Director.Create("Christopher Nolan");
        var releaseYearResult = ReleaseYear.Create(2010);
        var ratingResult = Rating.Create(8);
        var descriptionResult = Description.Create("Sci-fi thriller");
        
        if (titleResult.IsFailure || 
            genreResult.IsFailure || 
            directorResult.IsFailure || 
            releaseYearResult.IsFailure || 
            ratingResult.IsFailure || 
            descriptionResult.IsFailure)
        {
            throw new InvalidOperationException("Invalid data while creating value objects.");
        }
        
        var filmResult = Film.Create(
            FilmId.NewId,
            titleResult.Value,
            genreResult.Value,
            directorResult.Value,
            releaseYearResult.Value,
            ratingResult.Value,
            descriptionResult.Value);

        if (filmResult.IsFailure)
        {
            throw new InvalidOperationException("Failed to create film");
        }

        return filmResult.Value;
    }
    
    [Fact]
    public async Task GetFilms_ShouldReturnFilms_WhenRepositoryReturnsData()
    {
        // Arrange
        var films = new List<Film>
        {
            CreateFilm(),
            CreateFilm()
        };
        
        var filmsResponse = films.Select(film => new GetFilmsResponse
        {
            Id = film.Id.Value,
            FullName = new FullNameDto(film.Title.Value, film.Description.Value),
            Genre = new GenreDto(film.Genre.Value),
            Director = new DirectorDto(film.Director.Value),
            Rating = new RatingDto(film.Rating.Value),
            Release = new ReleaseYearDto(film.ReleaseYear.Value)
        });

        var query = new QueryObject();

        _filmsRepositoryMock
            .Setup(repo => repo.Get(
                query, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(filmsResponse.ToList());

        var handler = new GetFilmsHandler(_filmsRepositoryMock.Object);

        // Act
        var result = await handler.Handle(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Films.Count());
        
        Assert.Equal("Inception", result.Value.Films.ToList()[0].FullName.Name);
        Assert.Equal("Inception", result.Value.Films.ToList()[1].FullName.Name);
        
        _filmsRepositoryMock.Verify(repo => repo.Get(
            query,
            It.IsAny<CancellationToken>()), 
            Times.Once);
    }

    [Fact]
    public async Task GetFilms_ShouldReturnFailure_WhenRepositoryReturnsFailure()
    {
        // Arrange
        var error = Errors.General.ValueIsInvalid("Films");

        var query = new QueryObject();
        
        _filmsRepositoryMock
            .Setup(repo => repo.Get(
                query,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(error);

        var handler = new GetFilmsHandler(_filmsRepositoryMock.Object);

        // Act
        var result = await handler.Handle(query);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Films is invalid", result.Error.Message);
        
        _filmsRepositoryMock.Verify(repo => repo.Get(
            query,
                It.IsAny<CancellationToken>()), 
            Times.Once);
    }
}