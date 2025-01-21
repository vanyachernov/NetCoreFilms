using Films.Core.FilmManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Films.Infrastructure;

public class FilmDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public FilmDbContext(
        IConfiguration configuration,
        DbContextOptions<FilmDbContext> options) : base(options) 
            => _configuration = configuration;
    
    public DbSet<Film> Films { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FilmDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}