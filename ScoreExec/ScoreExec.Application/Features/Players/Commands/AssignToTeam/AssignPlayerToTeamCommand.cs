using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Players.Commands.AssignToTeam;

public record AssignPlayerToTeamCommand(Guid PlayerId, Guid TeamId) : IRequest<Result>;

public class AssignPlayerToTeamCommandHandler : IRequestHandler<AssignPlayerToTeamCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public AssignPlayerToTeamCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(AssignPlayerToTeamCommand request, CancellationToken cancellationToken)
    {
        var player = await _context.Players
            .Include(p => p.Teams)
            .FirstOrDefaultAsync(p => p.Id == request.PlayerId && p.TenantId == _currentUser.TenantId, cancellationToken);

        if (player == null) return Result.Failure("Player not found.", 404);

        var team = await _context.Teams
            .FirstOrDefaultAsync(t => t.Id == request.TeamId && t.TenantId == _currentUser.TenantId, cancellationToken);

        if (team == null) return Result.Failure("Team not found.", 404);

        if (player.Teams.Any(t => t.Id == team.Id))
        {
            return Result.Failure("Player is already in this team.");
        }

        player.Teams.Add(team);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
