using FluentValidation;

namespace Application.Features.Results.Commands.Create;

public class CreateResultCommandValidator : AbstractValidator<CreateResultCommand>
{
    public CreateResultCommandValidator()
    {
        RuleFor(c => c.QuizId).NotEmpty();
        RuleFor(c => c.WrongAnswers).NotEmpty();
        RuleFor(c => c.CorrectAnswers).NotEmpty();
    }
}
