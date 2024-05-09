using Application.Features.ApplicantBootcampContents.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.ApplicantBootcampContents.Constants.ApplicantBootcampContentsOperationClaims;

namespace Application.Features.ApplicantBootcampContents.Queries.GetList;

public class GetListApplicantBootcampContentQuery : IRequest<GetListResponse<GetListApplicantBootcampContentListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListApplicantBootcampContents({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetApplicantBootcampContents";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListApplicantBootcampContentQueryHandler : IRequestHandler<GetListApplicantBootcampContentQuery, GetListResponse<GetListApplicantBootcampContentListItemDto>>
    {
        private readonly IApplicantBootcampContentRepository _applicantBootcampContentRepository;
        private readonly IMapper _mapper;

        public GetListApplicantBootcampContentQueryHandler(IApplicantBootcampContentRepository applicantBootcampContentRepository, IMapper mapper)
        {
            _applicantBootcampContentRepository = applicantBootcampContentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListApplicantBootcampContentListItemDto>> Handle(GetListApplicantBootcampContentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ApplicantBootcampContent> applicantBootcampContents = await _applicantBootcampContentRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListApplicantBootcampContentListItemDto> response = _mapper.Map<GetListResponse<GetListApplicantBootcampContentListItemDto>>(applicantBootcampContents);
            return response;
        }
    }
}