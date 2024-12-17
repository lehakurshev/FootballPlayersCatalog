using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfiguration;

public class FootballPlayerConfiguration : IEntityTypeConfiguration<FootballPlayer>
{
    public void Configure(EntityTypeBuilder<FootballPlayer> builder)
    {
        builder.HasKey(footballPlayer => footballPlayer.Id);
        builder.HasIndex(note => note.Id).IsUnique();
        builder.Property(note => note.FirstName).HasMaxLength(100);
        builder.Property(note => note.LastName).HasMaxLength(100);
        builder.Property(note => note.Country).HasMaxLength(100);
        builder.Property(note => note.TeamName).HasMaxLength(100);
    }
}