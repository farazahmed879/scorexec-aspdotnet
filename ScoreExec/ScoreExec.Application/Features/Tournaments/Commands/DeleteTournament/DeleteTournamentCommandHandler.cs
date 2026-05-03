using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Tournaments.Commands.DeleteTournament;

public class DeleteTournamentCommandHandler : IRequestHandler<DeleteTournamentCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteTournamentCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<bool>> Handle(DeleteTournamentCommand request, CancellationToken cancellationToken)
    {
        var tenantId = _currentUserService.TenantId;
        var tournament = await _context.Tournaments
            .FirstOrDefaultAsync(t => t.Id == request.Id && t.TenantId == tenantId, cancellationToken);

        if (tournament == null)
        {
            return Result<bool>.Failure("Tournament not found.", 404);
        }

        _context.Tournaments.Remove(tournament);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
