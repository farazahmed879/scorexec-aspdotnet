using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Domain.Common;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Application.Features.Scoring.Commands.RecordDelivery;

public class RecordDeliveryCommandHandler : IRequestHandler<RecordDeliveryCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public RecordDeliveryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(RecordDeliveryCommand request, CancellationToken cancellationToken)
    {
        var innings = await _context.Innings
            .Include(i => i.Overs)
            .ThenInclude(o => o.Deliveries)
            .FirstOrDefaultAsync(i => i.Id == request.InningsId, cancellationToken);

        if (innings == null) return Result.Failure("Innings not found.", 404);

        var over = innings.Overs.FirstOrDefault(o => o.Id == request.OverId);
        if (over == null) return Result.Failure("Over not found.", 404);

        var delivery = Delivery.Create(
            request.OverId,
            request.BallNumber,
            request.BatsmanId,
            request.BowlerId,
            request.RunsScored,
            request.IsWide,
            request.IsNoBall,
            request.IsBye,
            request.IsLegBye,
            request.IsWicket,
            request.WicketType,
            request.WicketFielderId);

        await _context.Deliveries.AddAsync(delivery, cancellationToken);

        // Update Over stats
        int overRuns = over.Deliveries.Sum(d => d.RunsScored) + request.RunsScored;
        if (request.IsWide || request.IsNoBall) overRuns += 1; // Basic extras logic
        
        int overWickets = over.Deliveries.Count(d => d.IsWicket) + (request.IsWicket ? 1 : 0);
        over.UpdateStats(overRuns, overWickets);

        // Update Innings stats
        int totalRuns = innings.TotalRuns + request.RunsScored;
        if (request.IsWide || request.IsNoBall) totalRuns += 1;
        
        int totalWickets = innings.TotalWickets + (request.IsWicket ? 1 : 0);
        
        // Simple over calculation: (completed overs) + (balls in current over / 6)
        // This is a placeholder logic for decimal overs
        decimal totalOvers = over.OverNumber - 1 + (decimal)over.Deliveries.Count(d => !d.IsWide && !d.IsNoBall) / 10;
        if (!request.IsWide && !request.IsNoBall) totalOvers += 0.1m;

        innings.UpdateScore(totalRuns, totalWickets, totalOvers);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
