using Application.Features.BootcampImages.Commands.Create;
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


namespace Application.Features.BootcampVideos.Commands.Create;
public class CreateBootcampVideoCommand
    : IRequest<CreatedBootcampVideoResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int BootcampId { get; set; }
    public string ThumbnailUrl { get; set; }

    public string[] Roles => [Admin, Write, BootcampVideosOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBootcampImages"];

    public class CreateBootcampVideoCommandHandler : IRequestHandler<CreateBootcampVideoCommand, CreatedBootcampVideoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBootcampVideoRepository _bootcampVideoRepository;
        private readonly BootcampVideoBusinessRules _bootcampVideoBusinessRules;

        public CreateBootcampVideoCommandHandler(
            IMapper mapper,
            IBootcampVideoRepository bootcampVideoRepository,
            BootcampVideoBusinessRules bootcampVideoBusinessRules
        )
        {
            _mapper = mapper;
            _bootcampVideoRepository = bootcampVideoRepository;
            _bootcampVideoBusinessRules = bootcampVideoBusinessRules;
        }

        public async Task<CreatedBootcampVideoResponse> Handle(
            CreateBootcampVideoCommand request,
            CancellationToken cancellationToken
        )
        {
            BootcampVideo bootcampVideo = _mapper.Map<BootcampVideo>(request);

            await _bootcampVideoRepository.AddAsync(bootcampVideo);

            CreatedBootcampVideoResponse response = _mapper.Map<CreatedBootcampVideoResponse>(bootcampVideo);
            return response;
        }
    }
}