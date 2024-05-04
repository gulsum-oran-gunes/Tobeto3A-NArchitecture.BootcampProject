using FluentValidation;

namespace Application.Features.BootcampVideos.Commands.Update;

public class UpdateBootcampVideoCommandValidator : AbstractValidator<UpdateBootcampVideoCommand>
{
    public UpdateBootcampVideoCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BootcampId).NotEmpty();
        RuleFor(c => c.ThumbnailUrl).NotEmpty();
    }
}
