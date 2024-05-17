using Application.Features.BootcampContents.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.BootcampContents.Constants.BootcampContentsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Features.BootcampContents.Rules;

namespace Application.Features.BootcampContents.Queries.GetList;

public class GetListBootcampContentQuery : IRequest<GetListResponse<GetListBootcampContentListItemDto>> // ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] Roles => [Admin, Read];
    public bool BypassCache { get; }
    public string? CacheKey => $"GetListBootcampContents({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetBootcampContents";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListBootcampContentQueryHandler : IRequestHandler<GetListBootcampContentQuery, GetListResponse<GetListBootcampContentListItemDto>>
    {
        private readonly IBootcampContentRepository _bootcampContentRepository;
        private readonly IMapper _mapper;
        private readonly BootcampContentBusinessRules _bootcampContentBusinessRules;

        public GetListBootcampContentQueryHandler(IBootcampContentRepository bootcampContentRepository, 
            IMapper mapper, BootcampContentBusinessRules bootcampContentBusinessRules)
        {
            _bootcampContentRepository = bootcampContentRepository;
            _mapper = mapper;
            _bootcampContentBusinessRules = bootcampContentBusinessRules;
        }

        public async Task<GetListResponse<GetListBootcampContentListItemDto>> Handle(GetListBootcampContentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BootcampContent> bootcampContents = await _bootcampContentRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken,
                include: b => b.Include(b => b.Bootcamp)
            );
           
           
            GetListResponse<GetListBootcampContentListItemDto> response = _mapper.Map<GetListResponse<GetListBootcampContentListItemDto>>(bootcampContents);
            return response;


        }
    }
}