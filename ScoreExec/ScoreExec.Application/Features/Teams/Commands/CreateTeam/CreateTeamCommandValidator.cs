using FluentValidation;

namespace ScoreExec.Application.Features.Teams.Commands.CreateTeam;

public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Team name is required.")
            .MaximumLength(100).WithMessage("Team name must not exceed 100 characters.");
            
        RuleFor(x => x.HomeColor)
            .MaximumLength(50).WithMessage("Home color must not exceed 50 characters.");
            
        RuleFor(x => x.AwayColor)
            .MaximumLength(50).WithMessage("Away color must not exceed 50 characters.");
    }
}
