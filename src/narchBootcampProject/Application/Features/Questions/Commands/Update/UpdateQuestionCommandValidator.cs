using FluentValidation;

namespace Application.Features.Questions.Commands.Update;

public class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BootcampId).NotEmpty();
        RuleFor(c => c.Text).NotEmpty();
        RuleFor(c => c.AnswerA).NotEmpty();
        RuleFor(c => c.AnswerB).NotEmpty();
        RuleFor(c => c.AnswerC).NotEmpty();
        RuleFor(c => c.AnswerD).NotEmpty();
        RuleFor(c => c.CorrectAnswer).NotEmpty();
    }
}
