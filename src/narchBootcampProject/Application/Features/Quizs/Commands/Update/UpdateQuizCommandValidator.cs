using FluentValidation;

namespace Application.Features.Quizs.Commands.Update;

public class UpdateQuizCommandValidator : AbstractValidator<UpdateQuizCommand>
{
    public UpdateQuizCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ApplicantId).NotEmpty();
        RuleFor(c => c.BootcampId).NotEmpty();
        RuleFor(c => c.StartTime).NotEmpty();
        RuleFor(c => c.EndTime).NotEmpty();
    }
}
