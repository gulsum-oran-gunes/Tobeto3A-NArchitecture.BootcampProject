using Application.Features.Results.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.Results.Rules;

public class ResultBusinessRules : BaseBusinessRules
{
    private readonly IResultRepository _resultRepository;
    private readonly ILocalizationService _localizationService;

    public ResultBusinessRules(IResultRepository resultRepository, ILocalizationService localizationService)
    {
        _resultRepository = resultRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ResultsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ResultShouldExistWhenSelected(Result? result)
    {
        if (result == null)
            await throwBusinessException(ResultsBusinessMessages.ResultNotExists);
    }

    public async Task ResultIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Result? result = await _resultRepository.GetAsync(
            predicate: r => r.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ResultShouldExistWhenSelected(result);
    }
}
