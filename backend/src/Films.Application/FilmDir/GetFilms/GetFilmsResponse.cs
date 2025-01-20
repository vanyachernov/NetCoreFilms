using Films.Application.DTOs;

namespace Films.Application.FilmDir.GetFilms;

public class GetFilmsResponse
{
    public Guid Id { get; set; }
    public FullNameDto FullName { get; set; }
    public GenreDto Genre { get; set; }
    public DirectorDto Director { get; set; }
    public RatingDto Rating { get; set; }
    public ReleaseYearDto Release { get; set; }
}