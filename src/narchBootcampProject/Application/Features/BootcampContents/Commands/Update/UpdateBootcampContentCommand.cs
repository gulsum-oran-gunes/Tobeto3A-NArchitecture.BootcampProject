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

namespace Application.Features.BootcampContents.Commands.Update;

public class UpdateBootcampContentCommand : IRequest<UpdatedBootcampContentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int BootcampId { get; set; }
    public string? VideoUrl { get; set; }
    public string? Content { get; set; }

    public string[] Roles => [Admin, Write, BootcampContentsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBootcampContents"];

    public class UpdateBootcampContentCommandHandler : IRequestHandler<UpdateBootcampContentCommand, UpdatedBootcampContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBootcampContentRepository _bootcampContentRepository;
        private readonly BootcampContentBusinessRules _bootcampContentBusinessRules;

        public UpdateBootcampContentCommandHandler(IMapper mapper, IBootcampContentRepository bootcampContentRepository,
                                         BootcampContentBusinessRules bootcampContentBusinessRules)
        {
            _mapper = mapper;
            _bootcampContentRepository = bootcampContentRepository;
            _bootcampContentBusinessRules = bootcampContentBusinessRules;
        }

        public async Task<UpdatedBootcampContentResponse> Handle(UpdateBootcampContentCommand request, CancellationToken cancellationToken)
        {
            BootcampContent? bootcampContent = await _bootcampContentRepository.GetAsync(predicate: bc => bc.Id == request.Id, cancellationToken: cancellationToken);
            await _bootcampContentBusinessRules.BootcampContentShouldExistWhenSelected(bootcampContent);
            bootcampContent = _mapper.Map(request, bootcampContent);

            await _bootcampContentRepository.UpdateAsync(bootcampContent!);

            UpdatedBootcampContentResponse response = _mapper.Map<UpdatedBootcampContentResponse>(bootcampContent);
            return response;
        }
    }
}