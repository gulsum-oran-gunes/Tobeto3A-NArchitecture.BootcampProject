using FluentValidation;

namespace Application.Features.Questions.Commands.Create;

public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
    {
        RuleFor(c => c.BootcampId).NotEmpty();
        RuleFor(c => c.Text).NotEmpty();
        RuleFor(c => c.AnswerA).NotEmpty();
        RuleFor(c => c.AnswerB).NotEmpty();
        RuleFor(c => c.AnswerC).NotEmpty();
        RuleFor(c => c.AnswerD).NotEmpty();
        RuleFor(c => c.CorrectAnswer).NotEmpty();
    }
}
