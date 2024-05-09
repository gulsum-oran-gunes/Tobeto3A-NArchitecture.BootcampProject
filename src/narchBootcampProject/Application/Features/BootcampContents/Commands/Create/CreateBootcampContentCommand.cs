using Application.Features.BootcampContents.Constants;
using Application.Features.BootcampContents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.BootcampContents.Constants.BootcampContentsOperationClaims;

namespace Application.Features.BootcampContents.Commands.Create;

public class CreateBootcampContentCommand : IRequest<CreatedBootcampContentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int BootcampId { get; set; }
    public string? VideoUrl { get; set; }
    public string? Content { get; set; }

    public string[] Roles => [Admin, Write, BootcampContentsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBootcampContents"];

    public class CreateBootcampContentCommandHandler : IRequestHandler<CreateBootcampContentCommand, CreatedBootcampContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBootcampContentRepository _bootcampContentRepository;
        private readonly BootcampContentBusinessRules _bootcampContentBusinessRules;

        public CreateBootcampContentCommandHandler(IMapper mapper, IBootcampContentRepository bootcampContentRepository,
                                         BootcampContentBusinessRules bootcampContentBusinessRules)
        {
            _mapper = mapper;
            _bootcampContentRepository = bootcampContentRepository;
            _bootcampContentBusinessRules = bootcampContentBusinessRules;
        }

        public async Task<CreatedBootcampContentResponse> Handle(CreateBootcampContentCommand request, CancellationToken cancellationToken)
        {
            BootcampContent bootcampContent = _mapper.Map<BootcampContent>(request);

            await _bootcampContentRepository.AddAsync(bootcampContent);

            CreatedBootcampContentResponse response = _mapper.Map<CreatedBootcampContentResponse>(bootcampContent);
            return response;
        }
    }
}