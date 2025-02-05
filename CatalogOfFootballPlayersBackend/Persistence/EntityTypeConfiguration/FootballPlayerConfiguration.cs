using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfiguration;

public class FootballPlayerConfiguration : IEntityTypeConfiguration<FootballPlayer>
{
    public void Configure(EntityTypeBuilder<FootballPlayer> builder)
    {
        builder.HasKey(footballPlayer => footballPlayer.Id);
        builder.HasIndex(footballPlayer => footballPlayer.Id).IsUnique();
        builder.Property(footballPlayer => footballPlayer.FirstName).HasMaxLength(100);
        builder.Property(footballPlayer => footballPlayer.LastName).HasMaxLength(100);
        builder.Property(footballPlayer => footballPlayer.Country).HasMaxLength(100);
        builder.Property(footballPlayer => footballPlayer.TeamName).HasMaxLength(100);
    }
}