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

namespace Application.Features.ApplicantBootcampContents.Commands.Create;

public class CreateApplicantBootcampContentCommand : IRequest<CreatedApplicantBootcampContentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid ApplicantId { get; set; }
    public int BootcampContentId { get; set; }

    public string[] Roles => [Admin, Write, ApplicantBootcampContentsOperationClaims.Create, Student];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetApplicantBootcampContents"];

    public class CreateApplicantBootcampContentCommandHandler : IRequestHandler<CreateApplicantBootcampContentCommand, CreatedApplicantBootcampContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicantBootcampContentRepository _applicantBootcampContentRepository;
        private readonly ApplicantBootcampContentBusinessRules _applicantBootcampContentBusinessRules;

        public CreateApplicantBootcampContentCommandHandler(IMapper mapper, IApplicantBootcampContentRepository applicantBootcampContentRepository,
                                         ApplicantBootcampContentBusinessRules applicantBootcampContentBusinessRules)
        {
            _mapper = mapper;
            _applicantBootcampContentRepository = applicantBootcampContentRepository;
            _applicantBootcampContentBusinessRules = applicantBootcampContentBusinessRules;
        }

        public async Task<CreatedApplicantBootcampContentResponse> Handle(CreateApplicantBootcampContentCommand request, CancellationToken cancellationToken)
        {
            ApplicantBootcampContent applicantBootcampContent = _mapper.Map<ApplicantBootcampContent>(request);

            await _applicantBootcampContentRepository.AddAsync(applicantBootcampContent);

            CreatedApplicantBootcampContentResponse response = _mapper.Map<CreatedApplicantBootcampContentResponse>(applicantBootcampContent);
            return response;
        }
    }
}