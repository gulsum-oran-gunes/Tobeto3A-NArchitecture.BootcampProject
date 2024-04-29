using Application.Features.Quizs.Constants;
using Application.Features.Quizs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.Quizs.Constants.QuizsOperationClaims;

namespace Application.Features.Quizs.Commands.Update;

public class UpdateQuizCommand
    : IRequest<UpdatedQuizResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int BootcampId { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    public string[] Roles => [Admin, Write, QuizsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetQuizs"];

    public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, UpdatedQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuizRepository _quizRepository;
        private readonly QuizBusinessRules _quizBusinessRules;

        public UpdateQuizCommandHandler(IMapper mapper, IQuizRepository quizRepository, QuizBusinessRules quizBusinessRules)
        {
            _mapper = mapper;
            _quizRepository = quizRepository;
            _quizBusinessRules = quizBusinessRules;
        }

        public async Task<UpdatedQuizResponse> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
        {
            Quiz? quiz = await _quizRepository.GetAsync(predicate: q => q.Id == request.Id, cancellationToken: cancellationToken);
            await _quizBusinessRules.QuizShouldExistWhenSelected(quiz);
            quiz = _mapper.Map(request, quiz);

            await _quizRepository.UpdateAsync(quiz!);

            UpdatedQuizResponse response = _mapper.Map<UpdatedQuizResponse>(quiz);
            return response;
        }
    }
}
