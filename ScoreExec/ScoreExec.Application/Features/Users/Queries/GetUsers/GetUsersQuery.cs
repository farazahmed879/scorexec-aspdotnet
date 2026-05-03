using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Domain.Common;
using ScoreExec.Domain.Enums;

namespace ScoreExec.Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery() : IRequest<Result<List<UserDto>>>;

public record UserDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string Role,
    Guid TenantId,
    bool IsActive
);

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<List<UserDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetUsersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        // We need to cast the DbContext to access the Users directly if IApplicationDbContext doesn't have it
        // Or we use the context.Tenants and join if needed.
        // Actually, let's check IApplicationDbContext.
        return Result<List<UserDto>>.Failure("Not implemented yet - checking IApplicationDbContext", 500);
    }
}
