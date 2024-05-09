using FluentValidation;

namespace Application.Features.BootcampContents.Commands.Create;

public class CreateBootcampContentCommandValidator : AbstractValidator<CreateBootcampContentCommand>
{
    public CreateBootcampContentCommandValidator()
    {
        RuleFor(c => c.BootcampId).NotEmpty();
        RuleFor(c => c.VideoUrl).NotEmpty();
        RuleFor(c => c.Content).NotEmpty();
    }
}