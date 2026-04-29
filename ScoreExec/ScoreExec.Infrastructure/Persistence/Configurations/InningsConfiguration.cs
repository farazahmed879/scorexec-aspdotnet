using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Infrastructure.Persistence.Configurations;

public class InningsConfiguration : IEntityTypeConfiguration<Innings>
{
    public void Configure(EntityTypeBuilder<Innings> builder)
    {
        builder.ToTable("innings");
        builder.HasKey(i => i.Id);
        builder.HasIndex(i => i.MatchId);
        builder.HasQueryFilter(i => !i.IsDeleted);

        builder.HasOne(i => i.Match)
            .WithMany(m => m.Innings)
            .HasForeignKey(i => i.MatchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
