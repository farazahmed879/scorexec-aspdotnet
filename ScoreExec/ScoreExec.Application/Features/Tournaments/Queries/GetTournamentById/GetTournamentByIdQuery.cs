using MediatR;
using ScoreExec.Application.Features.Tournaments.DTOs;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Tournaments.Queries.GetTournamentById;

public record GetTournamentByIdQuery(Guid Id) : IRequest<Result<TournamentDto>>;
