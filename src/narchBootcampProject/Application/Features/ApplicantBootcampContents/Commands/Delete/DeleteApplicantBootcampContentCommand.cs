using Application.Features.ApplicantBootcampContents.Constants;
using Application.Features.ApplicantBootcampContents.Constants;
using Application.Features.ApplicantBootcampContents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ApplicantBootcampContents.Constants.ApplicantBootcampContentsOperationClaims;

namespace Application.Features.ApplicantBootcampContents.Commands.Delete;

public class DeleteApplicantBootcampContentCommand : IRequest<DeletedApplicantBootcampContentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, ApplicantBootcampContentsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetApplicantBootcampContents"];

    public class DeleteApplicantBootcampContentCommandHandler : IRequestHandler<DeleteApplicantBootcampContentCommand, DeletedApplicantBootcampContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicantBootcampContentRepository _applicantBootcampContentRepository;
        private readonly ApplicantBootcampContentBusinessRules _applicantBootcampContentBusinessRules;

        public DeleteApplicantBootcampContentCommandHandler(IMapper mapper, IApplicantBootcampContentRepository applicantBootcampContentRepository,
                                         ApplicantBootcampContentBusinessRules applicantBootcampContentBusinessRules)
        {
            _mapper = mapper;
            _applicantBootcampContentRepository = applicantBootcampContentRepository;
            _applicantBootcampContentBusinessRules = applicantBootcampContentBusinessRules;
        }

        public async Task<DeletedApplicantBootcampContentResponse> Handle(DeleteApplicantBootcampContentCommand request, CancellationToken cancellationToken)
        {
            ApplicantBootcampContent? applicantBootcampContent = await _applicantBootcampContentRepository.GetAsync(predicate: abc => abc.Id == request.Id, cancellationToken: cancellationToken);
            await _applicantBootcampContentBusinessRules.ApplicantBootcampContentShouldExistWhenSelected(applicantBootcampContent);

            await _applicantBootcampContentRepository.DeleteAsync(applicantBootcampContent!, true);

            DeletedApplicantBootcampContentResponse response = _mapper.Map<DeletedApplicantBootcampContentResponse>(applicantBootcampContent);
            return response;
        }
    }
}