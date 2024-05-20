using Application.Features.Bootcamps.Queries.GetList;
using Application.Features.Bootcamps.Queries.GetListByInstructorId;
using Application.Features.Certificates.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuestPDF.Helpers.Colors;

namespace Application.Features.Certificates.Queries.GetByApplicantId;
public class GetByApplicantIdQuery : IRequest<GetListResponse<GetListCertificateListItemDto>> //, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid ApplicantId { get; set; }
    //public string[] Roles => [Admin, Read];

    //public bool BypassCache { get; }
    //public string? CacheKey => $"GetListCertificates({PageRequest.PageIndex},{PageRequest.PageSize})";
    //public string? CacheGroupKey => "GetCertificates";
    //public TimeSpan? SlidingExpiration { get; }

    public class GetByApplicantIdQueryHandler
        : IRequestHandler<GetByApplicantIdQuery, GetListResponse<GetListCertificateListItemDto>>
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IMapper _mapper;

        public GetByApplicantIdQueryHandler(ICertificateRepository certificateRepository, IMapper mapper)
        {
            _certificateRepository = certificateRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCertificateListItemDto>> Handle(
            GetByApplicantIdQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Certificate> certificates = await _certificateRepository.GetListAsync(
                predicate: x => x.ApplicantId == request.ApplicantId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: p => p.Include(x => x.Applicant).Include(p => p.Bootcamp)
            );

            GetListResponse<GetListCertificateListItemDto> response = _mapper.Map<GetListResponse<GetListCertificateListItemDto>>(
                certificates
            );
            return response;
        }
    }
}
