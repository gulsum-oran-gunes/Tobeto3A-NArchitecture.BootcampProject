using Application.Features.Questions.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.Questions.Rules;

public class QuestionBusinessRules : BaseBusinessRules
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ILocalizationService _localizationService;

    public QuestionBusinessRules(IQuestionRepository questionRepository, ILocalizationService localizationService)
    {
        _questionRepository = questionRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, QuestionsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task QuestionShouldExistWhenSelected(Question? question)
    {
        if (question == null)
            await throwBusinessException(QuestionsBusinessMessages.QuestionNotExists);
    }

    public async Task QuestionIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Question? question = await _questionRepository.GetAsync(
            predicate: q => q.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await QuestionShouldExistWhenSelected(question);
    }
}
