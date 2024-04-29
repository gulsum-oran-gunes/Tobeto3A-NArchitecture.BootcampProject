using FluentValidation;

namespace Application.Features.Quizs.Commands.Finish;

public class FinishQuizCommandValidator : AbstractValidator<FinishQuizCommand>
{
    public FinishQuizCommandValidator() { }
}
