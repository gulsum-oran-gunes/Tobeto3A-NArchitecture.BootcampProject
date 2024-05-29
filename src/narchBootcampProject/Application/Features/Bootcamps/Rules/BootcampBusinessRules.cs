using Application.Features.Bootcamps.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.Bootcamps.Rules;

public class BootcampBusinessRules : BaseBusinessRules
{
    private readonly IBootcampRepository _bootcampRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IBootcampStateRepository _bootcampStateRepository;
    private readonly IInstructorRepository _instructorRepository;

    public BootcampBusinessRules(IBootcampRepository bootcampRepository, 
        ILocalizationService localizationService,
        IBootcampStateRepository bootcampStateRepository,
        IInstructorRepository instructorRepository)
    {
        _bootcampRepository = bootcampRepository;
        _localizationService = localizationService;
        _bootcampStateRepository = bootcampStateRepository;
        _instructorRepository= instructorRepository;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BootcampsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BootcampShouldExistWhenSelected(Bootcamp? bootcamp)
    {
        if (bootcamp == null)
            await throwBusinessException(BootcampsBusinessMessages.BootcampNotExists);
    }

    public async Task BootcampIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Bootcamp? bootcamp = await _bootcampRepository.GetAsync(
            predicate: b => b.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BootcampShouldExistWhenSelected(bootcamp);
    }
    public async Task CheckIfBootcampNameExists(string bootcampName)
    {
        var isExists = await _bootcampRepository.GetAsync(bootcamp => bootcamp.Name == bootcampName);
        if (isExists is not null) throw new BusinessException(BootcampsBusinessMessages.BootcampNameExists);
    }
    public async Task CheckIfBootcampStateIdExists(int bootcampStateId)
    {
        var isExists = await _bootcampStateRepository.GetAsync(x => x.Id == bootcampStateId);
        if (isExists is  null) throw new BusinessException(BootcampsBusinessMessages.BootcampStateExists);
    }
    public async Task CheckIfInstructorIdExists(Guid instructorId)
    {
        var isExists = await _instructorRepository.GetAsync(x => x.Id == instructorId);
        if (isExists is null) throw new BusinessException(BootcampsBusinessMessages.InstructorExists); 
    }
}
