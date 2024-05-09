using Application.Features.BootcampContents.Constants;
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

namespace Application.Features.BootcampContents.Commands.Delete;

public class DeleteBootcampContentCommand : IRequest<DeletedBootcampContentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, BootcampContentsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBootcampContents"];

    public class DeleteBootcampContentCommandHandler : IRequestHandler<DeleteBootcampContentCommand, DeletedBootcampContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBootcampContentRepository _bootcampContentRepository;
        private readonly BootcampContentBusinessRules _bootcampContentBusinessRules;

        public DeleteBootcampContentCommandHandler(IMapper mapper, IBootcampContentRepository bootcampContentRepository,
                                         BootcampContentBusinessRules bootcampContentBusinessRules)
        {
            _mapper = mapper;
            _bootcampContentRepository = bootcampContentRepository;
            _bootcampContentBusinessRules = bootcampContentBusinessRules;
        }

        public async Task<DeletedBootcampContentResponse> Handle(DeleteBootcampContentCommand request, CancellationToken cancellationToken)
        {
            BootcampContent? bootcampContent = await _bootcampContentRepository.GetAsync(predicate: bc => bc.Id == request.Id, cancellationToken: cancellationToken);
            await _bootcampContentBusinessRules.BootcampContentShouldExistWhenSelected(bootcampContent);

            await _bootcampContentRepository.DeleteAsync(bootcampContent!);

            DeletedBootcampContentResponse response = _mapper.Map<DeletedBootcampContentResponse>(bootcampContent);
            return response;
        }
    }
}