using MediatR;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Tournaments.DTOs;
using ScoreExec.Domain.Common;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Application.Features.Tournaments.Commands.CreateTournament;

public class CreateTournamentCommandHandler : IRequestHandler<CreateTournamentCommand, Result<TournamentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateTournamentCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result<TournamentDto>> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
    {
        var tournament = Tournament.Create(
            _currentUser.TenantId,
            request.Name,
            request.Description,
            request.Sport,
            request.Format,
            request.StartDate,
            request.EndDate,
            request.MaxTeams);

        await _context.Tournaments.AddAsync(tournament, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var dto = new TournamentDto(
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
            tournament.CreatedAt);

        return Result<TournamentDto>.Created(dto);
    }
}
