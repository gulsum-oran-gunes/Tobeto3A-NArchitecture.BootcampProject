using Application.Features.ApplicationEntities.Constants;
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
using static Application.Features.ApplicationEntities.Constants.ApplicationEntitiesOperationClaims;

namespace Application.Features.ApplicationEntities.Queries.GetList;

public class GetListApplicationEntityQuery
    : IRequest<GetListResponse<GetListApplicationEntityListItemDto>>,ISecuredRequest,ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read, Student];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListApplicationEntities({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetApplicationEntities";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListApplicationEntityQueryHandler
        : IRequestHandler<GetListApplicationEntityQuery, GetListResponse<GetListApplicationEntityListItemDto>>
    {
        private readonly IApplicationEntityRepository _applicationEntityRepository;
        private readonly IMapper _mapper;

        public GetListApplicationEntityQueryHandler(IApplicationEntityRepository applicationEntityRepository, IMapper mapper)
        {
            _applicationEntityRepository = applicationEntityRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListApplicationEntityListItemDto>> Handle(
            GetListApplicationEntityQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<ApplicationEntity> applicationEntities = await _applicationEntityRepository.GetListAsync(
                orderBy: query => query.OrderByDescending(b => b.CreatedDate),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                 include: p => p.Include(x => x.Applicant).Include(x => x.ApplicationState).Include(x => x.Bootcamp).ThenInclude(x => x.BootcampImages)
                .Include(x => x.Bootcamp).ThenInclude(x => x.Instructor));
           

            GetListResponse<GetListApplicationEntityListItemDto> response = 
                _mapper.Map<GetListResponse<GetListApplicationEntityListItemDto>>(applicationEntities);
            return response;
        }
    }
}
