using Application.Features.Results.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using NArchitecture.Core.Persistence.Paging;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Threading;

namespace Application.Features.Results.Rules;

public class ResultBusinessRules : BaseBusinessRules
{
    private readonly IResultRepository _resultRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IQuizRepository _quizRepository;

    public ResultBusinessRules(IResultRepository resultRepository, ILocalizationService localizationService, IQuizRepository quizRepository)
    {
        _resultRepository = resultRepository;
        _localizationService = localizationService;
        _quizRepository = quizRepository;
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
