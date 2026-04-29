using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Infrastructure.Persistence.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("players");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(p => p.LastName).IsRequired().HasMaxLength(50);
        builder.Property(p => p.BattingStyle).HasConversion<string>();
        builder.Property(p => p.BowlingStyle).HasConversion<string>();
        builder.HasIndex(p => p.TeamId);
        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.HasOne(p => p.Team)
            .WithMany(t => t.Players)
            .HasForeignKey(p => p.TeamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(p => p.FullName);
    }
}
