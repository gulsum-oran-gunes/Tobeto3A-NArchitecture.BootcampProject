using Application.Features.Results.Constants;
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

namespace Application.Features.Results.Commands.Delete;

public class DeleteResultCommand
    : IRequest<DeletedResultResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, ResultsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetResults"];

    public class DeleteResultCommandHandler : IRequestHandler<DeleteResultCommand, DeletedResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly IResultRepository _resultRepository;
        private readonly ResultBusinessRules _resultBusinessRules;

        public DeleteResultCommandHandler(
            IMapper mapper,
            IResultRepository resultRepository,
            ResultBusinessRules resultBusinessRules
        )
        {
            _mapper = mapper;
            _resultRepository = resultRepository;
            _resultBusinessRules = resultBusinessRules;
        }

        public async Task<DeletedResultResponse> Handle(DeleteResultCommand request, CancellationToken cancellationToken)
        {
            Result? result = await _resultRepository.GetAsync(
                predicate: r => r.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _resultBusinessRules.ResultShouldExistWhenSelected(result);

            await _resultRepository.DeleteAsync(result!);

            DeletedResultResponse response = _mapper.Map<DeletedResultResponse>(result);
            return response;
        }
    }
}
