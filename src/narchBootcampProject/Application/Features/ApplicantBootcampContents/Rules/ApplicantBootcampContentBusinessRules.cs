using Application.Features.ApplicantBootcampContents.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.ApplicantBootcampContents.Rules;

public class ApplicantBootcampContentBusinessRules : BaseBusinessRules
{
    private readonly IApplicantBootcampContentRepository _applicantBootcampContentRepository;
    private readonly ILocalizationService _localizationService;

    public ApplicantBootcampContentBusinessRules(IApplicantBootcampContentRepository applicantBootcampContentRepository, ILocalizationService localizationService)
    {
        _applicantBootcampContentRepository = applicantBootcampContentRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ApplicantBootcampContentsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ApplicantBootcampContentShouldExistWhenSelected(ApplicantBootcampContent? applicantBootcampContent)
    {
        if (applicantBootcampContent == null)
            await throwBusinessException(ApplicantBootcampContentsBusinessMessages.ApplicantBootcampContentNotExists);
    }

    public async Task ApplicantBootcampContentIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        ApplicantBootcampContent? applicantBootcampContent = await _applicantBootcampContentRepository.GetAsync(
            predicate: abc => abc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ApplicantBootcampContentShouldExistWhenSelected(applicantBootcampContent);
    }
}