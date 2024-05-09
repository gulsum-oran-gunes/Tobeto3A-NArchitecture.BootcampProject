using FluentValidation;

namespace Application.Features.ApplicantBootcampContents.Commands.Create;

public class CreateApplicantBootcampContentCommandValidator : AbstractValidator<CreateApplicantBootcampContentCommand>
{
    public CreateApplicantBootcampContentCommandValidator()
    {
        RuleFor(c => c.ApplicantId).NotEmpty();
        RuleFor(c => c.BootcampContentId).NotEmpty();
    }
}