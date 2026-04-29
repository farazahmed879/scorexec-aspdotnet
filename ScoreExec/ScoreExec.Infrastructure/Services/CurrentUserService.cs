using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ScoreExec.Application.Common.Interfaces;

namespace ScoreExec.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(value, out var id) ? id : Guid.Empty;
        }
    }

    public Guid TenantId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User?.FindFirstValue("tenant_id");
            return Guid.TryParse(value, out var id) ? id : Guid.Empty;
        }
    }

    public string Role =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}
