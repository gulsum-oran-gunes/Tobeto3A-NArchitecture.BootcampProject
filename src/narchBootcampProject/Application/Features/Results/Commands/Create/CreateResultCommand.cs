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

namespace Application.Features.Results.Commands.Create;

public class CreateResultCommand
    : IRequest<CreatedResultResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int QuizId { get; set; }
    public int WrongAnswers { get; set; }
    public int CorrectAnswers { get; set; }

    public string[] Roles => [Admin, Write, ResultsOperationClaims.Create, Student];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetResults"];

    public class CreateResultCommandHandler : IRequestHandler<CreateResultCommand, CreatedResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly IResultRepository _resultRepository;
        private readonly ResultBusinessRules _resultBusinessRules;

        public CreateResultCommandHandler(
            IMapper mapper,
            IResultRepository resultRepository,
            ResultBusinessRules resultBusinessRules
        )
        {
            _mapper = mapper;
            _resultRepository = resultRepository;
            _resultBusinessRules = resultBusinessRules;
        }

        public async Task<CreatedResultResponse> Handle(CreateResultCommand request, CancellationToken cancellationToken)
        {
            Result result = _mapper.Map<Result>(request);

            await _resultRepository.AddAsync(result);

            CreatedResultResponse response = _mapper.Map<CreatedResultResponse>(result);
            return response;
        }
    }
}
