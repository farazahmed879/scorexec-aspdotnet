using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Infrastructure.Persistence.Configurations;

public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
{
    public void Configure(EntityTypeBuilder<Tournament> builder)
    {
        builder.ToTable("tournaments");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
        builder.Property(t => t.Description).HasMaxLength(500);
        builder.Property(t => t.LogoUrl).HasMaxLength(500);
        builder.Property(t => t.Sport).HasConversion<string>();
        builder.Property(t => t.Format).HasConversion<string>();
        builder.Property(t => t.Type).HasConversion<string>();
        builder.Property(t => t.Status).HasConversion<string>();
        builder.HasIndex(t => t.TenantId);
        builder.HasQueryFilter(t => !t.IsDeleted);

        builder.HasOne(t => t.Tenant)
            .WithMany(t => t.Tournaments)
            .HasForeignKey(t => t.TenantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
