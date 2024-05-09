using Application.Features.ApplicantBootcampContents.Constants;
using Application.Features.ApplicantBootcampContents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ApplicantBootcampContents.Constants.ApplicantBootcampContentsOperationClaims;

namespace Application.Features.ApplicantBootcampContents.Queries.GetById;

public class GetByIdApplicantBootcampContentQuery : IRequest<GetByIdApplicantBootcampContentResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdApplicantBootcampContentQueryHandler : IRequestHandler<GetByIdApplicantBootcampContentQuery, GetByIdApplicantBootcampContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicantBootcampContentRepository _applicantBootcampContentRepository;
        private readonly ApplicantBootcampContentBusinessRules _applicantBootcampContentBusinessRules;

        public GetByIdApplicantBootcampContentQueryHandler(IMapper mapper, IApplicantBootcampContentRepository applicantBootcampContentRepository, ApplicantBootcampContentBusinessRules applicantBootcampContentBusinessRules)
        {
            _mapper = mapper;
            _applicantBootcampContentRepository = applicantBootcampContentRepository;
            _applicantBootcampContentBusinessRules = applicantBootcampContentBusinessRules;
        }

        public async Task<GetByIdApplicantBootcampContentResponse> Handle(GetByIdApplicantBootcampContentQuery request, CancellationToken cancellationToken)
        {
            ApplicantBootcampContent? applicantBootcampContent = await _applicantBootcampContentRepository.GetAsync(predicate: abc => abc.Id == request.Id, cancellationToken: cancellationToken);
            await _applicantBootcampContentBusinessRules.ApplicantBootcampContentShouldExistWhenSelected(applicantBootcampContent);

            GetByIdApplicantBootcampContentResponse response = _mapper.Map<GetByIdApplicantBootcampContentResponse>(applicantBootcampContent);
            return response;
        }
    }
}