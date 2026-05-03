using MediatR;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Matches.DTOs;
using ScoreExec.Domain.Common;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Application.Features.Matches.Commands.CreateMatch;

public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand, Result<MatchDto>>
{
    private readonly IApplicationDbContext _context;

    public CreateMatchCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<MatchDto>> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
    {
        var match = Match.Create(
            request.TournamentId,
            request.HomeTeamId,
            request.AwayTeamId,
            request.ScheduledAt,
            request.Venue,
            request.TotalOvers);

        await _context.Matches.AddAsync(match, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var dto = new MatchDto(
            match.Id,
            match.TournamentId,
            match.HomeTeamId,
            match.AwayTeamId,
            match.ScheduledAt,
            match.Venue,
            match.Status,
            match.TotalOvers,
            match.WinnerTeamId,
            match.TossWinnerId,
            match.TossDecision,
            match.CreatedAt);

        return Result<MatchDto>.Created(dto);
    }
}
