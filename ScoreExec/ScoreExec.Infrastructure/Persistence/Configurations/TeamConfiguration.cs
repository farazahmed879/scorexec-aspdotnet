using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Infrastructure.Persistence.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("teams");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
        builder.Property(t => t.LogoUrl).HasMaxLength(500);
        builder.Property(t => t.HomeColor).HasMaxLength(7);
        builder.Property(t => t.AwayColor).HasMaxLength(7);
        builder.HasIndex(t => t.TournamentId);
        builder.HasQueryFilter(t => !t.IsDeleted);

        builder.HasOne(t => t.Tournament)
            .WithMany(t => t.Teams)
            .HasForeignKey(t => t.TournamentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
