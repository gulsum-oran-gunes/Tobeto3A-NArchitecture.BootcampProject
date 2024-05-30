using System.Linq.Expressions;
using Application.Features.Questions.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.Questions;

public class QuestionManager : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly QuestionBusinessRules _questionBusinessRules;

    public QuestionManager(IQuestionRepository questionRepository, QuestionBusinessRules questionBusinessRules)
    {
        _questionRepository = questionRepository;
        _questionBusinessRules = questionBusinessRules;
    }

    public async Task<Question?> GetAsync(
        Expression<Func<Question, bool>> predicate,
        Func<IQueryable<Question>, IIncludableQueryable<Question, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Question? question = await _questionRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return question;
    }

    public async Task<IPaginate<Question>?> GetListAsync(
        Expression<Func<Question, bool>>? predicate = null,
        Func<IQueryable<Question>, IOrderedQueryable<Question>>? orderBy = null,
        Func<IQueryable<Question>, IIncludableQueryable<Question, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Question> questionList = await _questionRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return questionList;
    }

    public async Task<Question> AddAsync(Question question)
    {
        Question addedQuestion = await _questionRepository.AddAsync(question);

        return addedQuestion;
    }

    public async Task<Question> UpdateAsync(Question question)
    {
        Question updatedQuestion = await _questionRepository.UpdateAsync(question);

        return updatedQuestion;
    }

    public async Task<Question> DeleteAsync(Question question, bool permanent = false)
    {
        Question deletedQuestion = await _questionRepository.DeleteAsync(question);

        return deletedQuestion;
    }

    public async Task<List<Question>> GetAllAsync()
    {
        return await _questionRepository.GetAllAsync();
    }

    public async Task<List<Question>> GetRandomQuestionsByBootcampIdAsync(int bootcampId)
    {
        var random = new Random();
        var availableQuestions = await _questionRepository.GetAllAsync();
        var randomQuestions = availableQuestions
            .Where(q => q.BootcampId == bootcampId)
            .OrderBy(q => random.Next())
            .Take(5)
            .ToList();
        return randomQuestions;
    }
}
