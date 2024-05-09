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

    public BootcampContentBusinessRules(IBootcampContentRepository bootcampContentRepository, ILocalizationService localizationService)
    {
        _bootcampContentRepository = bootcampContentRepository;
        _localizationService = localizationService;
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
}