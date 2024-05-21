using Application.Features.Applicants.Rules;
using Application.Features.ApplicationEntities.Constants;
using Application.Features.ApplicationStates.Rules;
using Application.Features.Bootcamps.Rules;
using Application.Services.Applicants;
using Application.Services.ApplicationStates;
using Application.Services.Blacklists;
using Application.Services.Bootcamps;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.ApplicationEntities.Rules;

public class ApplicationEntityBusinessRules : BaseBusinessRules
{
    private readonly IApplicationEntityRepository _applicationEntityRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IBlacklistService _blacklistService;
    private readonly IApplicantService _applicantService;
    private readonly IBootcampService _bootcampService;
    private readonly IApplicationStateService _applicationStateService;

    public ApplicationEntityBusinessRules(IApplicationEntityRepository applicationEntityRepository, 
        ILocalizationService localizationService, IBlacklistService blacklistService, 
        IApplicantService applicantService, IBootcampService bootcampService,
        IApplicationStateService applicationStateService)
    {
        _applicationEntityRepository = applicationEntityRepository;
        _localizationService = localizationService;
        _blacklistService = blacklistService;
        _applicantService = applicantService;
        _bootcampService = bootcampService;
        _applicationStateService = applicationStateService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(
            messageKey,
            ApplicationEntitiesBusinessMessages.SectionName
        );
        throw new BusinessException(message);
    }

    public async Task ApplicationEntityShouldExistWhenSelected(ApplicationEntity? applicationEntity)
    {
        if (applicationEntity == null)
            await throwBusinessException(ApplicationEntitiesBusinessMessages.ApplicationEntityNotExists);
    }

    public async Task ApplicationEntityIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        ApplicationEntity? applicationEntity = await _applicationEntityRepository.GetAsync(
            predicate: ae => ae.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ApplicationEntityShouldExistWhenSelected(applicationEntity);
    }
    public async Task CheckIfApplicantBlacklist(Guid applicantId)
    {
        var applicant = await _blacklistService.GetAsync(predicate: a => a.ApplicantId == applicantId);
        if (applicant is not null) throw new BusinessException(ApplicationEntitiesBusinessMessages.ApplicantInBlacklist);
    }
    public async Task CheckIfApplicantExists(Guid applicantId)
    {
        var applicantIdExists = await _applicantService.GetAsync(predicate: a => a.Id == applicantId);
        if (applicantIdExists is null) throw new BusinessException(ApplicationEntitiesBusinessMessages.ApplicantIdNotExists);
    }
    public async Task CheckIfBootcampExists(int bootcampId)
    {
        var bootcamp = await _bootcampService.GetAsync(predicate: b => b.Id == bootcampId);
        if (bootcamp is null) throw new BusinessException(ApplicationEntitiesBusinessMessages.BootcampIdNotExists);
    }
    public async Task CheckIfApplicationStateExist(int applicationStateId)
    {
        var applicationState = await _applicationStateService.GetAsync(predicate: a => a.Id == applicationStateId);
        if (applicationState is null) throw new BusinessException(ApplicationEntitiesBusinessMessages.ApplicationStateIdNotExists);
    }
    public async Task CheckIfApplicantApplicationExists(Guid applicantId, int bootcampId)
    {
        var isExists = await _applicationEntityRepository.GetAsync(a => a.ApplicantId == applicantId && a.BootcampId == bootcampId);
        if (isExists is not null) throw new BusinessException(ApplicationEntitiesBusinessMessages.ApplicantApplicationExists);
    }
}
