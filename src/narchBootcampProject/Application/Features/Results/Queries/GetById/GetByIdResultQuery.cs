using Application.Features.Results.Constants;
using Application.Features.Results.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Results.Constants.ResultsOperationClaims;

namespace Application.Features.Results.Queries.GetById;

public class GetByIdResultQuery : IRequest<GetByIdResultResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdResultQueryHandler : IRequestHandler<GetByIdResultQuery, GetByIdResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly IResultRepository _resultRepository;
        private readonly ResultBusinessRules _resultBusinessRules;

        public GetByIdResultQueryHandler(
            IMapper mapper,
            IResultRepository resultRepository,
            ResultBusinessRules resultBusinessRules
        )
        {
            _mapper = mapper;
            _resultRepository = resultRepository;
            _resultBusinessRules = resultBusinessRules;
        }

        public async Task<GetByIdResultResponse> Handle(GetByIdResultQuery request, CancellationToken cancellationToken)
        {
            Result? result = await _resultRepository.GetAsync(
                predicate: r => r.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _resultBusinessRules.ResultShouldExistWhenSelected(result);

            GetByIdResultResponse response = _mapper.Map<GetByIdResultResponse>(result);
            return response;
        }
    }
}
