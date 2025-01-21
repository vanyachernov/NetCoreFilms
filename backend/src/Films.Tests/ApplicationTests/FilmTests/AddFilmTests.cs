using Films.Application.DTOs;
using Films.Application.FilmDir;
using Films.Application.FilmDir.AddFilm;
using Films.Core.FilmManagement;
using Moq;

namespace Films.Tests.ApplicationTests.FilmTests;

/// <summary>
/// Represents a tests for AddFilmHandler
/// </summary>
public class AddFilmTests
{
    private readonly Mock<IFilmsRepository> _filmsRepositoryMock = new();

    [Fact]
    public async Task AddFilm_ShouldSuccessExecution_WhenFilmHasValidData()
    {
        // Arrange
        var request = new AddFilmRequest
        {
            FullName = new FullNameDto("Inception", "Sci-fi thriller"),
            Genre = new GenreDto( "Science Fiction"),
            Director = new DirectorDto("Christopher Nolan"),
            Release = new ReleaseYearDto(2010),
            Rating = new RatingDto(8)
        };

        _filmsRepositoryMock
            .Setup(repo => repo.Create(
                It.IsAny<Film>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Guid.NewGuid());

        var handler = new AddFilmHandler(_filmsRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request);

        // Assert
        Assert.True(result.IsSuccess);
        _filmsRepositoryMock.Verify(repo => repo.Create(
            It.IsAny<Film>(), 
            It.IsAny<CancellationToken>()), 
            Times.Once);
    }
    
    [Fact]
    public async Task AddFilm_ShouldFailureExecution_WhenTitleIsInvalid()
    {
        // Arrange
        var request = new AddFilmRequest
        {
            FullName = new FullNameDto("", "Sci-fi thriller"),
            Genre = new GenreDto( "Science Fiction"),
            Director = new DirectorDto("Christopher Nolan"),
            Release = new ReleaseYearDto(2010),
            Rating = new RatingDto(8)
        };

        var handler = new AddFilmHandler(_filmsRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request);

        // Assert
        Assert.True(result.IsFailure);
        
        Assert.Equal(
            "Title is invalid", 
            result.Error.Message);
    }
    
    [Fact]
    public async Task AddFilm_ShouldFailureExecution_WhenReleaseYearIsInvalid()
    {
        // Arrange
        var request = new AddFilmRequest
        {
            FullName = new FullNameDto("Inception", "Sci-fi thriller"),
            Genre = new GenreDto( "Science Fiction"),
            Director = new DirectorDto("Christopher Nolan"),
            Release = new ReleaseYearDto(1887),
            Rating = new RatingDto(8)
        };

        var handler = new AddFilmHandler(_filmsRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            "Release Year is invalid", 
            result.Error.Message);
    }
    
    [Fact]
    public async Task AddFilm_ShouldFailureExecution_WhenRatingIsInvalid()
    {
        // Arrange
        var request = new AddFilmRequest
        {
            FullName = new FullNameDto("Inception", "Sci-fi thriller"),
            Genre = new GenreDto( "Science Fiction"),
            Director = new DirectorDto("Christopher Nolan"),
            Release = new ReleaseYearDto(2010),
            Rating = new RatingDto(0)
        };

        var handler = new AddFilmHandler(_filmsRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            "Rating is invalid", 
            result.Error.Message);
    }
}