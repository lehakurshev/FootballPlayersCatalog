using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfiguration;

public class TeamConfiguration  : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(team => team.Id);
        builder.HasIndex(team => team.Id).IsUnique();
        builder.HasIndex(team => team.Name).IsUnique();
        builder.Property(note => note.Name).HasMaxLength(100);
    }
}