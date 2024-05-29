using Application.Features.Applicants.Constants;
using Application.Features.Applicants.Rules;
using Application.Features.Blacklists.Constants;
using Application.Services.Applicants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.Blacklists.Rules;

public class BlacklistBusinessRules : BaseBusinessRules
{
    private readonly IBlacklistRepository _blacklistRepository;
    private readonly ILocalizationService _localizationService;
    private readonly ApplicantBusinessRules _applicantBusinessRules;
    private readonly IApplicantService _applicantService;   

    public BlacklistBusinessRules(IBlacklistRepository blacklistRepository, ApplicantBusinessRules applicantBusinessRules,
        ILocalizationService localizationService, IApplicantService applicantService)
    {
        _blacklistRepository = blacklistRepository;
        _localizationService = localizationService;
        _applicantBusinessRules = _applicantBusinessRules;
        _applicantService = applicantService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BlacklistsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BlacklistShouldExistWhenSelected(Blacklist? blacklist)
    {
        if (blacklist == null)
            await throwBusinessException(BlacklistsBusinessMessages.BlacklistNotExists);
    }

    public async Task BlacklistIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Blacklist? blacklist = await _blacklistRepository.GetAsync(
            predicate: b => b.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BlacklistShouldExistWhenSelected(blacklist);
    }
    public async Task CheckIfApplicantIdExists(Guid applicantId)
    {
        var isExists = await _applicantService.GetAsync(applicant => applicant.Id == applicantId);
        if (isExists is null) throw new BusinessException(ApplicantsBusinessMessages.ApplicantNotExists);
    }
    public async Task ChechIfReasonNull(string reason)
    {
        var isNull = await _blacklistRepository.GetAsync(r => r.Reason == reason);
        if (isNull is null) throw new BusinessException(BlacklistsBusinessMessages.ReasonNull);
    }
}
