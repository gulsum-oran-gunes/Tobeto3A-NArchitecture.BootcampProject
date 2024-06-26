using Application.Features.BootcampStates.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.BootcampStates.Rules;

public class BootcampStateBusinessRules : BaseBusinessRules
{
    private readonly IBootcampStateRepository _bootcampStateRepository;
    private readonly ILocalizationService _localizationService;

    public BootcampStateBusinessRules(IBootcampStateRepository bootcampStateRepository, ILocalizationService localizationService)
    {
        _bootcampStateRepository = bootcampStateRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BootcampStatesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BootcampStateShouldExistWhenSelected(BootcampState? bootcampState)
    {
        if (bootcampState == null)
            await throwBusinessException(BootcampStatesBusinessMessages.BootcampStateNotExists);
    }

    public async Task BootcampStateIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        BootcampState? bootcampState = await _bootcampStateRepository.GetAsync(
            predicate: bs => bs.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BootcampStateShouldExistWhenSelected(bootcampState);
    }
    public async Task CheckIfBootcampStateNameExists(string bootcampStateName)
    {
        var isExits = await _bootcampStateRepository.GetAsync(x => x.Name == bootcampStateName);
        if (isExits is not null) throw new BusinessException(BootcampStatesBusinessMessages.BootcampStateNameExists);
    }
}
