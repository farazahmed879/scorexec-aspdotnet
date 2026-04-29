namespace ScoreExec.API.Models.Auth;

public record LoginRequest(
    string Email,
    string Password
);
