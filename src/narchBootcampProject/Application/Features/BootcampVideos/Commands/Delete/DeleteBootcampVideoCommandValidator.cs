using Application.Features.BootcampImages.Commands.Delete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BootcampVideos.Commands.Delete;
public class DeleteBootcampVideoCommandValidator : AbstractValidator<DeleteBootcampVideoCommand>
{
    public DeleteBootcampVideoCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}