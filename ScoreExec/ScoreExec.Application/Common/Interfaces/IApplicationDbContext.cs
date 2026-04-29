using Microsoft.EntityFrameworkCore;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Tenant> Tenants { get; }
    DbSet<Tournament> Tournaments { get; }
    DbSet<Team> Teams { get; }
    DbSet<Player> Players { get; }
    DbSet<Match> Matches { get; }
    DbSet<Innings> Innings { get; }
    DbSet<Over> Overs { get; }
    DbSet<Delivery> Deliveries { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
