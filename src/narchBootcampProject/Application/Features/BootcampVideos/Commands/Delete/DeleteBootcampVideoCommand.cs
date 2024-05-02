using Application.Features.BootcampImages.Commands.Delete;
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

namespace Application.Features.BootcampVideos.Commands.Delete;
public class DeleteBootcampVideoCommand
    : IRequest<DeletedBootcampVideoResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, BootcampVideosOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBootcampVideos"];

    public class DeleteBootcampVideoCommandHandler : IRequestHandler<DeleteBootcampVideoCommand, DeletedBootcampVideoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBootcampVideoRepository _bootcampVideoRepository;
        private readonly BootcampVideoBusinessRules _bootcampVideoBusinessRules;

        public DeleteBootcampVideoCommandHandler(
            IMapper mapper,
            IBootcampVideoRepository bootcampVideoRepository,
            BootcampVideoBusinessRules bootcampVideoBusinessRules
        )
        {
            _mapper = mapper;
            _bootcampVideoRepository = bootcampVideoRepository;
            _bootcampVideoBusinessRules = bootcampVideoBusinessRules;
        }

        public async Task<DeletedBootcampVideoResponse> Handle(
            DeleteBootcampVideoCommand request,
            CancellationToken cancellationToken
        )
        {
            BootcampVideo? bootcampVideo = await _bootcampVideoRepository.GetAsync(
                predicate: bi => bi.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _bootcampVideoBusinessRules.BootcampVideoShouldExistWhenSelected(bootcampVideo);

            await _bootcampVideoRepository.DeleteAsync(bootcampVideo!, true);

            DeletedBootcampVideoResponse response = _mapper.Map<DeletedBootcampVideoResponse>(bootcampVideo);
            return response;
        }
    }
}