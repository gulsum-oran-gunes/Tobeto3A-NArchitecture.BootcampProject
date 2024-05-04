using Application.Features.BootcampImages.Constants;
using Application.Features.BootcampVideos.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BootcampVideos.Rules;
public class BootcampVideoBusinessRules : BaseBusinessRules
{
    private readonly IBootcampVideoRepository _bootcampVideoRepository;
    private readonly ILocalizationService _localizationService;

    public BootcampVideoBusinessRules(IBootcampVideoRepository bootcampVideoRepository, ILocalizationService localizationService)
    {
        _bootcampVideoRepository = bootcampVideoRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BootcampVideosBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BootcampVideoShouldExistWhenSelected(BootcampVideo? bootcampVideo)
    {
        if (bootcampVideo == null)
            await throwBusinessException(BootcampVideosBusinessMessages.BootcampVideoNotExists);
    }

    public async Task BootcampVideoIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        BootcampVideo? bootcampVideo = await _bootcampVideoRepository.GetAsync(
            predicate: bi => bi.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BootcampVideoShouldExistWhenSelected(bootcampVideo);
    }
}