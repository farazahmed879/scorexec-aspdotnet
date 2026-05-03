using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Matches.DTOs;
using ScoreExec.Domain.Common;
using ScoreExec.Domain.Entities;
using ScoreExec.Domain.Enums;

namespace ScoreExec.Application.Features.Matches.Commands.StartMatch;

public record StartMatchCommand(
    Guid MatchId,
    Guid TossWinnerId,
    TossDecision TossDecision,
    Guid OpeningBowlerId) : IRequest<Result>;

public class StartMatchCommandHandler : IRequestHandler<StartMatchCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public StartMatchCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(StartMatchCommand request, CancellationToken cancellationToken)
    {
        var match = await _context.Matches
            .Include(m => m.Innings)
            .FirstOrDefaultAsync(m => m.Id == request.MatchId, cancellationToken);

        if (match == null) return Result.Failure("Match not found.", 404);

        if (match.Status != MatchStatus.Scheduled)
            return Result.Failure("Match has already been started or completed.");

        // Set toss
        match.SetToss(request.TossWinnerId, request.TossDecision);

        // Determine batting/bowling teams based on toss
        Guid battingTeamId, bowlingTeamId;
        if (request.TossDecision == TossDecision.Bat)
        {
            battingTeamId = request.TossWinnerId;
            bowlingTeamId = request.TossWinnerId == match.HomeTeamId ? match.AwayTeamId : match.HomeTeamId;
        }
        else
        {
            bowlingTeamId = request.TossWinnerId;
            battingTeamId = request.TossWinnerId == match.HomeTeamId ? match.AwayTeamId : match.HomeTeamId;
        }

        // Start match
        match.Start();

        // Create 1st innings
        var innings = Innings.Create(match.Id, battingTeamId, bowlingTeamId, 1);
        await _context.Innings.AddAsync(innings, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken); // Save to get innings ID

        // Create 1st over
        var over = Over.Create(innings.Id, 1, request.OpeningBowlerId);
        await _context.Overs.AddAsync(over, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
