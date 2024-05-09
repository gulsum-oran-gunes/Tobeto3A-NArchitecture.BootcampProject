using FluentValidation;

namespace Application.Features.BootcampContents.Commands.Update;

public class UpdateBootcampContentCommandValidator : AbstractValidator<UpdateBootcampContentCommand>
{
    public UpdateBootcampContentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BootcampId).NotEmpty();
        RuleFor(c => c.VideoUrl).NotEmpty();
        RuleFor(c => c.Content).NotEmpty();
    }
}