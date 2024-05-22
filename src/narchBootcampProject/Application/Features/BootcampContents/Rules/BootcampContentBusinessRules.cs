using Application.Features.BootcampContents.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.BootcampContents.Rules;

public class BootcampContentBusinessRules : BaseBusinessRules
{
    private readonly IBootcampContentRepository _bootcampContentRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IApplicantBootcampContentRepository _applicantBootcampContentRepository;
    private readonly ICertificateRepository _certificateRepository;
    

    public BootcampContentBusinessRules(IBootcampContentRepository bootcampContentRepository,
        ILocalizationService localizationService,
        IApplicantBootcampContentRepository applicantBootcampContentRepository,
        ICertificateRepository certificateRepository)
    {
        _bootcampContentRepository = bootcampContentRepository;
        _localizationService = localizationService;
        _applicantBootcampContentRepository = applicantBootcampContentRepository;
        _certificateRepository = certificateRepository;
       
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BootcampContentsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BootcampContentShouldExistWhenSelected(BootcampContent? bootcampContent)
    {
        if (bootcampContent == null)
            await throwBusinessException(BootcampContentsBusinessMessages.BootcampContentNotExists);
    }

    public async Task BootcampContentIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        BootcampContent? bootcampContent = await _bootcampContentRepository.GetAsync(
            predicate: bc => bc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BootcampContentShouldExistWhenSelected(bootcampContent);
    }

    public bool HasApplicantBootcampContent(Guid? applicantId, int? bootcampContentId)
    {
        var applicantBootcampContent = _applicantBootcampContentRepository.Get(
            predicate: abc => abc.ApplicantId == applicantId && abc.BootcampContentId == bootcampContentId,
            enableTracking: false
        );

        return applicantBootcampContent != null; 

        // Eðer ilgili kayýt varsa true, yoksa false dön.
        // Content sayfasýnda kullanýcý içeriði izledim iþaretlemiþ mi bunu kontrol etmek ve checkboxu dolu getirmek için
    }

    public bool IfApplicantPassed(Guid? applicantId, int? bootcampId)
    {
        var certificate = _certificateRepository.Any(
            predicate: abc => abc.ApplicantId == applicantId && abc.BootcampId == bootcampId,
            enableTracking: false
        );

        return certificate;

    }


}