using FluentValidation;

namespace Application.Features.Results.Commands.Delete;

public class DeleteResultCommandValidator : AbstractValidator<DeleteResultCommand>
{
    public DeleteResultCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
