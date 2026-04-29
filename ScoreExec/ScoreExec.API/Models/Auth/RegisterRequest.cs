namespace ScoreExec.API.Models.Auth;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string TenantName,
    string TenantSlug
);
