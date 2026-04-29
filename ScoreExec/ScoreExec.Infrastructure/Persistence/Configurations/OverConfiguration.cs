using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Infrastructure.Persistence.Configurations;

public class OverConfiguration : IEntityTypeConfiguration<Over>
{
    public void Configure(EntityTypeBuilder<Over> builder)
    {
        builder.ToTable("overs");
        builder.HasKey(o => o.Id);
        builder.HasIndex(o => o.InningsId);
        builder.HasQueryFilter(o => !o.IsDeleted);

        builder.HasOne(o => o.Innings)
            .WithMany(i => i.Overs)
            .HasForeignKey(o => o.InningsId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Bowler)
            .WithMany()
            .HasForeignKey(o => o.BowlerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
