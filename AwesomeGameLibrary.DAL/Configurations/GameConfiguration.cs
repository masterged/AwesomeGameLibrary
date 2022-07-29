using AwesomeGameLibrary.Domain.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeGameLibrary.DAL.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Genre)
            .WithMany(x => x.Games)
            .HasForeignKey(x=>x.GenreId);
        
        builder.HasOne(x => x.Platform)
            .WithMany(x => x.Games)
            .HasForeignKey(x=>x.PlatformId);
    }
}