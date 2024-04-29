using Application.Features.Results.Constants;
using Application.Features.Results.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.Results.Constants.ResultsOperationClaims;

namespace Application.Features.Results.Commands.Update;

public class UpdateResultCommand
    : IRequest<UpdatedResultResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public int WrongAnswers { get; set; }
    public int CorrectAnswers { get; set; }

    public string[] Roles => [Admin, Write, ResultsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetResults"];

    public class UpdateResultCommandHandler : IRequestHandler<UpdateResultCommand, UpdatedResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly IResultRepository _resultRepository;
        private readonly ResultBusinessRules _resultBusinessRules;

        public UpdateResultCommandHandler(
            IMapper mapper,
            IResultRepository resultRepository,
            ResultBusinessRules resultBusinessRules
        )
        {
            _mapper = mapper;
            _resultRepository = resultRepository;
            _resultBusinessRules = resultBusinessRules;
        }

        public async Task<UpdatedResultResponse> Handle(UpdateResultCommand request, CancellationToken cancellationToken)
        {
            Result? result = await _resultRepository.GetAsync(
                predicate: r => r.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _resultBusinessRules.ResultShouldExistWhenSelected(result);
            result = _mapper.Map(request, result);

            await _resultRepository.UpdateAsync(result!);

            UpdatedResultResponse response = _mapper.Map<UpdatedResultResponse>(result);
            return response;
        }
    }
}
