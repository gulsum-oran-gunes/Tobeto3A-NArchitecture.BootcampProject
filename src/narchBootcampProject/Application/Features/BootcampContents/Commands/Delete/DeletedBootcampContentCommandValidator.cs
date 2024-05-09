using FluentValidation;

namespace Application.Features.BootcampContents.Commands.Delete;

public class DeleteBootcampContentCommandValidator : AbstractValidator<DeleteBootcampContentCommand>
{
    public DeleteBootcampContentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}