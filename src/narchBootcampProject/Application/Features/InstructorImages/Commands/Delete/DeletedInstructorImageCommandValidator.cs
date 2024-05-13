using FluentValidation;

namespace Application.Features.InstructorImages.Commands.Delete;

public class DeleteInstructorImageCommandValidator : AbstractValidator<DeleteInstructorImageCommand>
{
    public DeleteInstructorImageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}