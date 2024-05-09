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

namespace Application.Features.ApplicantBootcampContents.Commands.Update;

public class UpdateApplicantBootcampContentCommand : IRequest<UpdatedApplicantBootcampContentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int BootcampContentId { get; set; }

    public string[] Roles => [Admin, Write, ApplicantBootcampContentsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetApplicantBootcampContents"];

    public class UpdateApplicantBootcampContentCommandHandler : IRequestHandler<UpdateApplicantBootcampContentCommand, UpdatedApplicantBootcampContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicantBootcampContentRepository _applicantBootcampContentRepository;
        private readonly ApplicantBootcampContentBusinessRules _applicantBootcampContentBusinessRules;

        public UpdateApplicantBootcampContentCommandHandler(IMapper mapper, IApplicantBootcampContentRepository applicantBootcampContentRepository,
                                         ApplicantBootcampContentBusinessRules applicantBootcampContentBusinessRules)
        {
            _mapper = mapper;
            _applicantBootcampContentRepository = applicantBootcampContentRepository;
            _applicantBootcampContentBusinessRules = applicantBootcampContentBusinessRules;
        }

        public async Task<UpdatedApplicantBootcampContentResponse> Handle(UpdateApplicantBootcampContentCommand request, CancellationToken cancellationToken)
        {
            ApplicantBootcampContent? applicantBootcampContent = await _applicantBootcampContentRepository.GetAsync(predicate: abc => abc.Id == request.Id, cancellationToken: cancellationToken);
            await _applicantBootcampContentBusinessRules.ApplicantBootcampContentShouldExistWhenSelected(applicantBootcampContent);
            applicantBootcampContent = _mapper.Map(request, applicantBootcampContent);

            await _applicantBootcampContentRepository.UpdateAsync(applicantBootcampContent!);

            UpdatedApplicantBootcampContentResponse response = _mapper.Map<UpdatedApplicantBootcampContentResponse>(applicantBootcampContent);
            return response;
        }
    }
}