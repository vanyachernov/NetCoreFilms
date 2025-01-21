using Films.Application.DTOs;
using Films.Application.FilmDir;
using Films.Application.FilmDir.AddFilm;
using Films.Application.FilmDir.UpdateFilm;
using Films.Core.FilmManagement;
using Films.Core.FilmManagement.ValueObjects;
using Films.Core.Shared;
using Films.Core.Shared.IDs;
using Moq;

namespace Films.Tests.ApplicationTests.FilmTests;

/// <summary>
/// Represents a tests for UpdateFilmHandler
/// </summary>
public class UpdateFilmTests
{
    private readonly Mock<IFilmsRepository> _filmsRepositoryMock = new();

    private AddFilmRequest CreateAddFilmRequest()
    {
        return new AddFilmRequest
        {
            FullName = new FullNameDto("Inception", "Sci-fi thriller"),
            Genre = new GenreDto("Science Fiction"),
            Director = new DirectorDto("Christopher Nolan"),
            Rating = new RatingDto(8),
            Release = new ReleaseYearDto(2010)
        };
    }

    [Fact]
    public async Task UpdateFilm_ShouldReturnFilmId_WhenFilmExistsAndUpdatedSuccessfully()
    {
        // Arrange
        var filmId = Guid.NewGuid();
        var filmRequest = CreateAddFilmRequest();
        
        var existingFilm = Film.Create(
            FilmId.Create(filmId), 
            Title.Create("Inception").Value, 
            Genre.Create("Science Fiction").Value,
            Director.Create("Christopher Nolan").Value, 
            ReleaseYear.Create(2010).Value, 
            Rating.Create(8).Value, 
            Description.Create("Sci-fi thriller").Value);
        
        _filmsRepositoryMock
            .Setup(repo => repo.IsExists(
                It.IsAny<Guid>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _filmsRepositoryMock
            .Setup(repo => repo.Update(
                It.IsAny<Film>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingFilm);

        var handler = new UpdateFilmHandler(_filmsRepositoryMock.Object);

        // Act
        var result = await handler.Handle(
            filmId, 
            filmRequest);

        // Assert
        Assert.True(result.IsSuccess);
        
        Assert.Equal(
            filmId, 
            result.Value);
    }
    
    [Fact]
    public async Task UpdateFilm_ShouldReturnError_WhenFilmDoesNotExist()
    {
        // Arrange
        var filmId = Guid.NewGuid();
        var filmRequest = CreateAddFilmRequest();
        
        _filmsRepositoryMock
            .Setup(repo => repo.IsExists(
                It.IsAny<Guid>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var handler = new UpdateFilmHandler(_filmsRepositoryMock.Object);

        // Act
        var result = await handler.Handle(
            filmId, 
            filmRequest);

        // Assert
        Assert.True(result.IsFailure);
        
        Assert.Equal(
            Errors.General.NotFound(filmId).Message, 
            result.Error.Message);
    }
}