using Application.Features.BootcampImages.Commands.Update;
using Application.Features.BootcampImages.Constants;
using Application.Features.BootcampImages.Rules;
using Application.Features.BootcampVideos.Constants;
using Application.Features.BootcampVideos.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.BootcampVideos.Constants.BootcampVideosOperationClaims;

namespace Application.Features.BootcampVideos.Commands.Update;
public class UpdateBootcampVideoCommand
    : IRequest<UpdatedBootcampVideoResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public int BootcampId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ThumbnailUrl { get; set; }

    public string[] Roles => [Admin, Write, BootcampVideosOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBootcampVideos"];

    public class UpdateBootcampVideoCommandHandler : IRequestHandler<UpdateBootcampVideoCommand, UpdatedBootcampVideoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBootcampVideoRepository _bootcampVideoRepository;
        private readonly BootcampVideoBusinessRules _bootcampVideoBusinessRules;

        public UpdateBootcampVideoCommandHandler(
            IMapper mapper,
            IBootcampVideoRepository bootcampVideoRepository,
            BootcampVideoBusinessRules bootcampVideoBusinessRules
        )
        {
            _mapper = mapper;
            _bootcampVideoRepository = bootcampVideoRepository;
            _bootcampVideoBusinessRules = bootcampVideoBusinessRules;
        }

        public async Task<UpdatedBootcampVideoResponse> Handle(
            UpdateBootcampVideoCommand request,
            CancellationToken cancellationToken
        )
        {
            BootcampVideo? bootcampVideo = await _bootcampVideoRepository.GetAsync(
                predicate: bi => bi.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _bootcampVideoBusinessRules.BootcampVideoShouldExistWhenSelected(bootcampVideo);
            bootcampVideo = _mapper.Map(request, bootcampVideo);

            await _bootcampVideoRepository.UpdateAsync(bootcampVideo!);

            UpdatedBootcampVideoResponse response = _mapper.Map<UpdatedBootcampVideoResponse>(bootcampVideo);
            return response;
        }
    }
}

