using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Domain.Common;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Application.Features.Matches.Commands.CreateInnings;

public record CreateInningsCommand(
    Guid MatchId,
    Guid BattingTeamId,
    Guid BowlingTeamId,
    Guid OpeningBowlerId) : IRequest<Result>;

public class CreateInningsCommandHandler : IRequestHandler<CreateInningsCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public CreateInningsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CreateInningsCommand request, CancellationToken cancellationToken)
    {
        var match = await _context.Matches
            .Include(m => m.Innings)
            .FirstOrDefaultAsync(m => m.Id == request.MatchId, cancellationToken);

        if (match == null) return Result.Failure("Match not found.", 404);

        int nextInningsNumber = match.Innings.Count + 1;

        var innings = Innings.Create(match.Id, request.BattingTeamId, request.BowlingTeamId, nextInningsNumber);
        await _context.Innings.AddAsync(innings, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        // Create 1st over of this innings
        var over = Over.Create(innings.Id, 1, request.OpeningBowlerId);
        await _context.Overs.AddAsync(over, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
