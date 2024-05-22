using Application.Features.ApplicationEntities.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using System.Threading;

namespace Application.Features.ApplicationEntities.Rules;

public class ApplicationEntityBusinessRules : BaseBusinessRules
{
    private readonly IApplicationEntityRepository _applicationEntityRepository;
    private readonly ILocalizationService _localizationService;

    public ApplicationEntityBusinessRules(
        IApplicationEntityRepository applicationEntityRepository,
        ILocalizationService localizationService
    )
    {
        _applicationEntityRepository = applicationEntityRepository;
        _localizationService = localizationService;
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

    public bool IfApplicantApplied( int? bootcampId,Guid? applicantId)
    {
        var application = _applicationEntityRepository.Get(
            predicate: abc => abc.ApplicantId == applicantId && abc.BootcampId == bootcampId,
            enableTracking: false
        );

        return application != null;

        // Eðer ilgili kayýt varsa true, yoksa false dön.
        //Detail sayfasýnda o kullanýcý baþvurabilir mi kontrol etmek için
    }

}
