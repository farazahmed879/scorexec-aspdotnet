using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Tournaments.DTOs;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Tournaments.Commands.UpdateTournament;

public class UpdateTournamentCommandHandler : IRequestHandler<UpdateTournamentCommand, Result<TournamentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateTournamentCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<TournamentDto>> Handle(UpdateTournamentCommand request, CancellationToken cancellationToken)
    {
        var tenantId = _currentUserService.TenantId;
        var tournament = await _context.Tournaments
            .FirstOrDefaultAsync(t => t.Id == request.Id && t.TenantId == tenantId, cancellationToken);

        if (tournament == null)
        {
            return Result<TournamentDto>.Failure("Tournament not found.", 404);
        }

        tournament.Update(
            request.Name,
            request.Description,
            request.Sport,
            request.Format,
            request.StartDate,
            request.EndDate,
            request.MaxTeams);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<TournamentDto>.Success(new TournamentDto(
            tournament.Id,
            tournament.TenantId,
            tournament.Name,
            tournament.Description,
            tournament.Sport,
            tournament.Format,
            tournament.StartDate,
            tournament.EndDate,
            tournament.Status,
            tournament.MaxTeams,
            tournament.CreatedAt));
    }
}
