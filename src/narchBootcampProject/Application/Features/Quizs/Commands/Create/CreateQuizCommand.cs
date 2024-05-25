using Application.Features.QuizQuestions.Commands.Create;
using Application.Features.Quizs.Constants;
using Application.Features.Quizs.Rules;
using Application.Services.Questions;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.Quizs.Constants.QuizsOperationClaims;

namespace Application.Features.Quizs.Commands.Create;

public class CreateQuizCommand
    : IRequest<CreatedQuizResponse>,
        //ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public Guid ApplicantId { get; set; }
    public int BootcampId { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    public string[] Roles => [Admin, Write, QuizsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetQuizs"];

    public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, CreatedQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuizRepository _quizRepository;
        private readonly QuizBusinessRules _quizBusinessRules;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionService _questionService;
        private readonly IQuizQuestionRepository _quizQuestionRepository;
        private readonly IBootcampRepository _bootcampRepository;

        public CreateQuizCommandHandler(
            IMapper mapper,
            IQuizRepository quizRepository,
            QuizBusinessRules quizBusinessRules,
            IQuestionRepository questionRepository,
            IQuestionService questionService,
            IQuizQuestionRepository quizQuestionRepository,
            IBootcampRepository bootcampRepository
        )
        {
            _mapper = mapper;
            _quizRepository = quizRepository;
            _quizBusinessRules = quizBusinessRules;
            _questionRepository = questionRepository;
            _questionService = questionService;
            _quizQuestionRepository = quizQuestionRepository;
            _bootcampRepository = bootcampRepository;
        }

        public async Task<CreatedQuizResponse> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            List<Question> randomQuestions = await _questionService.GetRandomQuestionsByBootcampIdAsync(request.BootcampId);
            var bootcamp = await _bootcampRepository.GetAsync(b => b.Id == request.BootcampId);
            Quiz quiz = _mapper.Map<Quiz>(request);
           
           
            await _quizRepository.AddAsync(quiz);

            foreach (var question in randomQuestions)
            {
                QuizQuestion quizQuestion = new QuizQuestion(quiz.Id, question.Id);
                await _quizQuestionRepository.AddAsync(quizQuestion);
            }

            CreatedQuizResponse response = _mapper.Map<CreatedQuizResponse>(quiz);
            response.BootcampName = bootcamp.Name;
            response.QuestionResponses = randomQuestions
                .Select(q =>
                {
                    var questionResponse = new QuestionResponse
                    {
                        Id = q.Id,
                        Text = q.Text,
                        AnswerA = q.AnswerA,
                        AnswerB = q.AnswerB,
                        AnswerC = q.AnswerC,
                        AnswerD = q.AnswerD,
                        BootcampId = q.BootcampId,
                    };
                    return questionResponse;
                })
                .ToList();

            return response;
        }
    }
}
