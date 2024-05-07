using Application.Features.Results.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using static Application.Features.Results.Constants.ResultsOperationClaims;

namespace Application.Features.Results.Queries.GetList;

public class GetListResultQuery : IRequest<GetListResponse<GetListResultListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListResults({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetResults";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListResultQueryHandler : IRequestHandler<GetListResultQuery, GetListResponse<GetListResultListItemDto>>
    {
        private readonly IResultRepository _resultRepository;
        private readonly IMapper _mapper;

        public GetListResultQueryHandler(IResultRepository resultRepository, IMapper mapper)
        {
            _resultRepository = resultRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListResultListItemDto>> Handle(
            GetListResultQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Result> results = await _resultRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(a => a.Quiz).ThenInclude(b => b.Applicant)
            );

            GetListResponse<GetListResultListItemDto> response = _mapper.Map<GetListResponse<GetListResultListItemDto>>(results);
            return response;
        }
    }
}
