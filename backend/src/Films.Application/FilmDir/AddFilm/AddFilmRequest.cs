using Films.Application.DTOs;

namespace Films.Application.FilmDir.AddFilm;

public class AddFilmRequest
{
    public FullNameDto FullName { get; set; }
    public GenreDto Genre { get; set; }
    public DirectorDto Director { get; set; }
    public RatingDto Rating { get; set; }
    public ReleaseYearDto Release { get; set; }
};