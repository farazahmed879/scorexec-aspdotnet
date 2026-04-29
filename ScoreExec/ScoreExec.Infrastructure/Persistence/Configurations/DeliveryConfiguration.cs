using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Infrastructure.Persistence.Configurations;

public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.ToTable("deliveries");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.WicketType).HasConversion<string>();
        builder.HasIndex(d => d.OverId);
        builder.HasQueryFilter(d => !d.IsDeleted);

        builder.HasOne(d => d.Over)
            .WithMany(o => o.Deliveries)
            .HasForeignKey(d => d.OverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.Batsman)
            .WithMany()
            .HasForeignKey(d => d.BatsmanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.Bowler)
            .WithMany()
            .HasForeignKey(d => d.BowlerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
