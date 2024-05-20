using Application.Features.Quizs.Commands.Create;
using Application.Features.Quizs.Constants;
using Application.Features.Quizs.Rules;
using Application.Services.Questions;
using Application.Services.QuizQuestions;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.Quizs.Constants.QuizsOperationClaims;

namespace Application.Features.Quizs.Commands.Finish;

public class FinishQuizCommand
    : IRequest<FinishedQuizResponse>,
        //ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int QuizId { get; set; }
    public Dictionary<int, string> Answers { get; set; }
    public string[] Roles => [Admin, Write, QuizsOperationClaims.Finish];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetQuizs"];

    public class FinishCommandHandler : IRequestHandler<FinishQuizCommand, FinishedQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly QuizBusinessRules _quizBusinessRules;
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizQuestionService _quizQuestionService;
        private readonly IResultRepository _resultRepository;

        public FinishCommandHandler(
            IMapper mapper,
            QuizBusinessRules quizBusinessRules,
            IQuizRepository quizRepository,
            IQuizQuestionService quizQuestionService,
            IResultRepository resultRepository
        )
        {
            _mapper = mapper;
            _quizBusinessRules = quizBusinessRules;
            _quizRepository = quizRepository;
            _quizQuestionService = quizQuestionService;
            _resultRepository = resultRepository;
        }

        public async Task<FinishedQuizResponse> Handle(FinishQuizCommand request, CancellationToken cancellationToken)
        {
            List<QuizQuestion> quizQuestions = await _quizQuestionService.GetQuizQuestionsByQuizIdAsync(request.QuizId);
            List<Question> questions = quizQuestions.Select(qq => qq.Question).ToList();
            int correctAnswersCount = 0;
            int wrongAnswersCount = 0;

            foreach (var qq in quizQuestions)
            {
                string answer;
                if (request.Answers.TryGetValue(qq.QuestionId, out answer))
                {
                    if (qq.Question.CorrectAnswer == answer)
                    {
                        correctAnswersCount++; // Do�ru cevaplar� say
                    }
                    else
                    {
                        wrongAnswersCount++; // Yanl�� cevaplar� say
                    }
                }
                else
                {
                    throw new Exception("Girdi�iniz soru idleri bu quizdeki sorular ile uyu�muyor.");
                }
            }
            bool isPassed = correctAnswersCount > 1;
            

            Result result = new Result
            {
                CorrectAnswers = correctAnswersCount,
                WrongAnswers = wrongAnswersCount,
                QuizId = request.QuizId,
                IsPassed = isPassed,
               
            };
            await _resultRepository.AddAsync(result);

            FinishedQuizResponse response = _mapper.Map<FinishedQuizResponse>(result);
            response.Result = result;
            response.QuestionResults = questions
                .Select(q =>
                {
                    var questionResult = new QuestionResult
                    {
                        Id = q.Id,
                        CorrectAnswer = q.CorrectAnswer,
                        StudentAnswer = request.Answers.ContainsKey(q.Id) ? request.Answers[q.Id] : ""
                        /*string.Join(", ", request.Answers.Values)*/
                        //string.Join(", ", questions.Where(q => q.Id $"{request.Answers[q.Id]}"))
                        //string.Join(", ", questions.Select(q =>
                        //{
                        //    var answer = request.Answers.ContainsKey(q.Id) ? request.Answers[q.Id] : ""; // Get answer for question ID
                        //    return answer;
                        //}))
                    };
                    return questionResult;
                })
                .ToList();

            return response;
        }
    }
}
