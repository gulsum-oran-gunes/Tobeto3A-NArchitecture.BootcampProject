using FluentValidation;

namespace Application.Features.InstructorImages.Commands.Update;

public class UpdateInstructorImageCommandValidator : AbstractValidator<UpdateInstructorImageCommand>
{
    public UpdateInstructorImageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.InstructorId).NotEmpty();
        RuleFor(c => c.ImagePath).NotEmpty();
    }
}