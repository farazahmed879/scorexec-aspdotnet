using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Infrastructure.Persistence.Configurations;

public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.ToTable("matches");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Venue).IsRequired().HasMaxLength(200);
        builder.Property(m => m.Status).HasConversion<string>();
        builder.Property(m => m.TossDecision).HasConversion<string>();
        builder.HasIndex(m => m.TournamentId);
        builder.HasQueryFilter(m => !m.IsDeleted);

        builder.HasOne(m => m.Tournament)
            .WithMany(t => t.Matches)
            .HasForeignKey(m => m.TournamentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.HomeTeam)
            .WithMany()
            .HasForeignKey(m => m.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.AwayTeam)
            .WithMany()
            .HasForeignKey(m => m.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
