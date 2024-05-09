using FluentValidation;

namespace Application.Features.ApplicantBootcampContents.Commands.Delete;

public class DeleteApplicantBootcampContentCommandValidator : AbstractValidator<DeleteApplicantBootcampContentCommand>
{
    public DeleteApplicantBootcampContentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}