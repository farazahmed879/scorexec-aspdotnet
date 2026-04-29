using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Infrastructure.Persistence.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("tenants");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
        builder.Property(t => t.Slug).IsRequired().HasMaxLength(50);
        builder.HasIndex(t => t.Slug).IsUnique();
        builder.Property(t => t.SubscriptionPlan).HasConversion<string>();
        builder.HasQueryFilter(t => !t.IsDeleted);
    }
}
