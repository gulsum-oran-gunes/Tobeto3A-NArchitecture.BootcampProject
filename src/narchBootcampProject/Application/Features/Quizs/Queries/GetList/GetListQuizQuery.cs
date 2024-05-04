using Application.Features.Quizs.Constants;
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
using static Application.Features.Quizs.Constants.QuizsOperationClaims;

namespace Application.Features.Quizs.Queries.GetList;

public class GetListQuizQuery : IRequest<GetListResponse<GetListQuizListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListQuizs({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetQuizs";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListQuizQueryHandler : IRequestHandler<GetListQuizQuery, GetListResponse<GetListQuizListItemDto>>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;

        public GetListQuizQueryHandler(IQuizRepository quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListQuizListItemDto>> Handle(
            GetListQuizQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Quiz> quizs = await _quizRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(a => a.Applicant).Include(b => b.Bootcamp)
            );

            GetListResponse<GetListQuizListItemDto> response = _mapper.Map<GetListResponse<GetListQuizListItemDto>>(quizs);
            return response;
        }
    }
}
