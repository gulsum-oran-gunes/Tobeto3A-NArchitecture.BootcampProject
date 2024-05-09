using FluentValidation;

namespace Application.Features.ApplicantBootcampContents.Commands.Update;

public class UpdateApplicantBootcampContentCommandValidator : AbstractValidator<UpdateApplicantBootcampContentCommand>
{
    public UpdateApplicantBootcampContentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ApplicantId).NotEmpty();
        RuleFor(c => c.BootcampContentId).NotEmpty();
    }
}