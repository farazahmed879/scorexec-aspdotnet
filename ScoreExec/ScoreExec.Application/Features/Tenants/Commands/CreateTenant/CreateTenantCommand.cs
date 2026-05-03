using MediatR;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Tenants.Commands.CreateTenant;

public record CreateTenantCommand(string Name, string Slug) : IRequest<Result<Guid>>;
