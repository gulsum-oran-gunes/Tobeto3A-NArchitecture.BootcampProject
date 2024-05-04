using Application.Features.BootcampImages.Commands.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BootcampVideos.Commands.Create;
public class CreateBootcampVideoCommandValidator : AbstractValidator<CreateBootcampVideoCommand>
{
    public CreateBootcampVideoCommandValidator()
    {
        RuleFor(c => c.BootcampId).NotEmpty();
        RuleFor(c => c.ThumbnailUrl).NotEmpty();
    }
}