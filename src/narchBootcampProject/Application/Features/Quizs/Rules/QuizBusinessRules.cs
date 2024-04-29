using Application.Features.Quizs.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.Quizs.Rules;

public class QuizBusinessRules : BaseBusinessRules
{
    private readonly IQuizRepository _quizRepository;
    private readonly ILocalizationService _localizationService;

    public QuizBusinessRules(IQuizRepository quizRepository, ILocalizationService localizationService)
    {
        _quizRepository = quizRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, QuizsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task QuizShouldExistWhenSelected(Quiz? quiz)
    {
        if (quiz == null)
            await throwBusinessException(QuizsBusinessMessages.QuizNotExists);
    }

    public async Task QuizIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Quiz? quiz = await _quizRepository.GetAsync(
            predicate: q => q.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await QuizShouldExistWhenSelected(quiz);
    }
}
