using Application.Features.BootcampContents.Rules;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BootcampContents.Queries.GetList;

public class HasApplicantBootcampContentResolver : IValueResolver<BootcampContent, GetListBootcampContentListItemDto, bool>
{
    private readonly BootcampContentBusinessRules _bootcampContentBusinessRules;
    private readonly Guid? _applicantId;
    private readonly int? _bootcampContentId;

    public HasApplicantBootcampContentResolver(BootcampContentBusinessRules bootcampContentBusinessRules, Guid? applicantId, int? bootcampContentId)
    {
        _bootcampContentBusinessRules = bootcampContentBusinessRules;
        _applicantId = applicantId;
        _bootcampContentId = bootcampContentId;
    }

    public bool Resolve(BootcampContent source, GetListBootcampContentListItemDto destination, bool destMember, ResolutionContext context)
    {
        return _bootcampContentBusinessRules.HasApplicantBootcampContent(_applicantId, _bootcampContentId, CancellationToken.None).Result; // Assuming async method
    }
}
