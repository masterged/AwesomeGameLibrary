using AwesomeGameLibrary.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwesomeGameLibrary.DAL.Contexts;

public class AwesomeDbContext : DbContext
{
    public AwesomeDbContext(DbContextOptions<AwesomeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Platform> Platforms { get; set; }
}