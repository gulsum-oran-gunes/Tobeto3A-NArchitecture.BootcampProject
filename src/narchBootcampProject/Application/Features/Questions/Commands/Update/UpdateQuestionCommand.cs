using Application.Features.Questions.Constants;
using Application.Features.Questions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.Questions.Constants.QuestionsOperationClaims;

namespace Application.Features.Questions.Commands.Update;

public class UpdateQuestionCommand
    : IRequest<UpdatedQuestionResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public int BootcampId { get; set; }
    public string Text { get; set; }
    public string AnswerA { get; set; }
    public string AnswerB { get; set; }
    public string AnswerC { get; set; }
    public string AnswerD { get; set; }
    public string CorrectAnswer { get; set; }

    public string[] Roles => [Admin, Write, QuestionsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetQuestions"];

    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, UpdatedQuestionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;
        private readonly QuestionBusinessRules _questionBusinessRules;

        public UpdateQuestionCommandHandler(
            IMapper mapper,
            IQuestionRepository questionRepository,
            QuestionBusinessRules questionBusinessRules
        )
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _questionBusinessRules = questionBusinessRules;
        }

        public async Task<UpdatedQuestionResponse> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            Question? question = await _questionRepository.GetAsync(
                predicate: q => q.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _questionBusinessRules.QuestionShouldExistWhenSelected(question);
            question = _mapper.Map(request, question);

            await _questionRepository.UpdateAsync(question!);

            UpdatedQuestionResponse response = _mapper.Map<UpdatedQuestionResponse>(question);
            return response;
        }
    }
}
