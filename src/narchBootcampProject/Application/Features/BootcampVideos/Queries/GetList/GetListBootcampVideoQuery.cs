using Application.Features.BootcampImages.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.BootcampVideos.Constants.BootcampVideosOperationClaims;


namespace Application.Features.BootcampVideos.Queries.GetList;
public class GetListBootcampVideoQuery
    : IRequest<GetListResponse<GetListBootcampVideoListItemDto>>,
        ISecuredRequest,
        ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListBootcampVideos({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetBootcampVideos";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListBootcampVideoQueryHandler
        : IRequestHandler<GetListBootcampVideoQuery, GetListResponse<GetListBootcampVideoListItemDto>>
    {
        private readonly IBootcampVideoRepository _bootcampVideoRepository;
        private readonly IMapper _mapper;

        public GetListBootcampVideoQueryHandler(IBootcampVideoRepository bootcampVideoRepository, IMapper mapper)
        {
            _bootcampVideoRepository = bootcampVideoRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBootcampVideoListItemDto>> Handle(
            GetListBootcampVideoQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<BootcampVideo> bootcampVideos = await _bootcampVideoRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListBootcampVideoListItemDto> response = _mapper.Map<
                GetListResponse<GetListBootcampVideoListItemDto>
            >(bootcampVideos);
            return response;
        }
    }
}