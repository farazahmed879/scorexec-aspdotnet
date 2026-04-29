namespace ScoreExec.API.Models.Auth;

public record AuthResponse(
    string Token,
    string Email,
    string FullName,
    string Role,
    Guid TenantId,
    DateTime ExpiresAt
);
