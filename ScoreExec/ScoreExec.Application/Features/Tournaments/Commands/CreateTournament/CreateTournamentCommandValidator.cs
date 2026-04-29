using FluentValidation;

namespace ScoreExec.Application.Features.Tournaments.Commands.CreateTournament;

public class CreateTournamentCommandValidator : AbstractValidator<CreateTournamentCommand>
{
    public CreateTournamentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tournament name is required.")
            .MaximumLength(100).WithMessage("Tournament name must not exceed 100 characters.");

        RuleFor(x => x.StartDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Start date must be in the future.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date.");

        RuleFor(x => x.MaxTeams)
            .GreaterThan(1).WithMessage("Max teams must be greater than 1.")
            .LessThanOrEqualTo(64).WithMessage("Max teams cannot exceed 64.");
    }
}
