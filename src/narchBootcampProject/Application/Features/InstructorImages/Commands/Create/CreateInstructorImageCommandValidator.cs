using FluentValidation;

namespace Application.Features.InstructorImages.Commands.Create;

public class CreateInstructorImageCommandValidator : AbstractValidator<CreateInstructorImageCommand>
{
    public CreateInstructorImageCommandValidator()
    {
        RuleFor(c => c.InstructorId).NotEmpty();
        RuleFor(c => c.ImagePath).NotEmpty();
    }
}