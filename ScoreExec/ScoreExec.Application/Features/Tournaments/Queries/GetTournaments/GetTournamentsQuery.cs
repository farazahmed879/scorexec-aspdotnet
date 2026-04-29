using MediatR;
using ScoreExec.Application.Features.Tournaments.DTOs;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Tournaments.Queries.GetTournaments;

public record GetTournamentsQuery() : IRequest<Result<List<TournamentDto>>>;
