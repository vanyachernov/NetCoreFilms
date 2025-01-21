using Films.Application.FilmDir;
using Films.Application.FilmDir.DeleteFilm;
using Moq;

namespace Films.Tests.ApplicationTests.FilmTests;

/// <summary>
/// Represents a tests for DeleteFilmHandler
/// </summary>
public class DeleteFilmTests
{
    private readonly Mock<IFilmsRepository> _filmsRepositoryMock = new();
    
    [Fact]
    public async Task DeleteFilm_ShouldReturnFilmId_WhenFilmExistsAndDeletedSuccessfully()
    {
        // Arrange
        var filmId = Guid.NewGuid();
    
        _filmsRepositoryMock
            .Setup(repo => repo.IsExists(
                It.IsAny<Guid>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _filmsRepositoryMock
            .Setup(repo => repo.Delete(
                It.IsAny<Guid>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(filmId);

        var handler = new DeleteFilmHandler(_filmsRepositoryMock.Object);

        // Act
        var result = await handler.Handle(filmId);

        // Assert
        Assert.True(result.IsSuccess);
        
        Assert.Equal(
            filmId, 
            result.Value);
    }
}