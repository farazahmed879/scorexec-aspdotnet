using MediatR;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Players.Queries.GetPlayerProfile;

public record GetPlayerProfileQuery(
    Guid PlayerId, 
    Guid? TournamentId = null, 
    Guid? TeamId = null, 
    DateTime? StartDate = null, 
    DateTime? EndDate = null
) : IRequest<Result<PlayerProfileDto>>;
