using Application.Features.BootcampImages.Queries.GetById;
using Application.Features.BootcampImages.Rules;
using Application.Features.BootcampVideos.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.BootcampVideos.Constants.BootcampVideosOperationClaims;


namespace Application.Features.BootcampVideos.Queries.GetById;
public class GetByIdBootcampVideoQuery : IRequest<GetByIdBootcampVideoResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdBootcampVideoQueryHandler : IRequestHandler<GetByIdBootcampVideoQuery, GetByIdBootcampVideoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBootcampVideoRepository _bootcampVideoRepository;
        private readonly BootcampVideoBusinessRules _bootcampVideoBusinessRules;

        public GetByIdBootcampVideoQueryHandler(
            IMapper mapper,
            IBootcampVideoRepository bootcampVideoRepository,
            BootcampVideoBusinessRules bootcampVideoBusinessRules
        )
        {
            _mapper = mapper;
            _bootcampVideoRepository = bootcampVideoRepository;
            _bootcampVideoBusinessRules = bootcampVideoBusinessRules;
        }

        public async Task<GetByIdBootcampVideoResponse> Handle(
            GetByIdBootcampVideoQuery request,
            CancellationToken cancellationToken
        )
        {
            BootcampVideo? bootcampVideo = await _bootcampVideoRepository.GetAsync(
                predicate: bi => bi.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _bootcampVideoBusinessRules.BootcampVideoShouldExistWhenSelected(bootcampVideo);

            GetByIdBootcampVideoResponse response = _mapper.Map<GetByIdBootcampVideoResponse>(bootcampVideo);
            return response;
        }
    }
}