using Films.Core.FilmManagement;
using Films.Core.Shared;
using Films.Core.Shared.IDs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Films.Infrastructure.Configurations;

public class FilmConfiguration : IEntityTypeConfiguration<Film>
{
    public void Configure(EntityTypeBuilder<Film> builder)
    {
        builder.ToTable("films");

        builder.HasKey(f => f.Id);
        
        builder
            .Property(q => q.Id)
            .HasConversion(
                id => id.Value,
                value => FilmId.Create(value));

        builder.ComplexProperty(f => f.Title, tf =>
        {
            tf
                .Property(tff => tff.Value)
                .HasMaxLength(Constants.MAX_FILM_NAME_TEXT_LENGTH)
                .HasColumnName("film_name")
                .IsRequired();
        });

        builder.ComplexProperty(f => f.Genre, tf =>
        {
            tf
                .Property(tff => tff.Value)
                .HasColumnName("film_genre")
                .IsRequired();
        });

        builder.ComplexProperty(f => f.ReleaseYear, tf =>
        {
            tf
                .Property(tff => tff.Value)
                .HasColumnName("film_release_year")
                .IsRequired();
        });

        builder.ComplexProperty(f => f.Director, tf =>
        {
            tf
                .Property(tff => tff.Value)
                .HasColumnName("film_director")
                .IsRequired();
        });

        builder.ComplexProperty(f => f.Description, tf =>
        {
            tf
                .Property(tff => tff.Value)
                .HasColumnName("film_description")
                .IsRequired();
        });

        builder.ComplexProperty(f => f.Rating, tf =>
        {
            tf
                .Property(tff => tff.Value)
                .HasColumnName("film_rating")
                .IsRequired();
        });
    }
}