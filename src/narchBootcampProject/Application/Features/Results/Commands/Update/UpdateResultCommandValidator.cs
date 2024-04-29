using FluentValidation;

namespace Application.Features.Results.Commands.Update;

public class UpdateResultCommandValidator : AbstractValidator<UpdateResultCommand>
{
    public UpdateResultCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.QuizId).NotEmpty();
        RuleFor(c => c.WrongAnswers).NotEmpty();
        RuleFor(c => c.CorrectAnswers).NotEmpty();
    }
}
