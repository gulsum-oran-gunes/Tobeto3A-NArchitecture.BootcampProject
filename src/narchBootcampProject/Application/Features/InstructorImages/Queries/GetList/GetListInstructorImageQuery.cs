using Application.Features.InstructorImages.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.InstructorImages.Constants.InstructorImagesOperationClaims;

namespace Application.Features.InstructorImages.Queries.GetList;

public class GetListInstructorImageQuery : IRequest<GetListResponse<GetListInstructorImageListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListInstructorImages({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetInstructorImages";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListInstructorImageQueryHandler : IRequestHandler<GetListInstructorImageQuery, GetListResponse<GetListInstructorImageListItemDto>>
    {
        private readonly IInstructorImageRepository _instructorImageRepository;
        private readonly IMapper _mapper;

        public GetListInstructorImageQueryHandler(IInstructorImageRepository instructorImageRepository, IMapper mapper)
        {
            _instructorImageRepository = instructorImageRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListInstructorImageListItemDto>> Handle(GetListInstructorImageQuery request, CancellationToken cancellationToken)
        {
            IPaginate<InstructorImage> instructorImages = await _instructorImageRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListInstructorImageListItemDto> response = _mapper.Map<GetListResponse<GetListInstructorImageListItemDto>>(instructorImages);
            return response;
        }
    }
}