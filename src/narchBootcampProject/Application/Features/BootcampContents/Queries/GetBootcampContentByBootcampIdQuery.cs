using Application.Features.ApplicantBootcampContents.Rules;
using Application.Features.BootcampContents.Queries.GetList;
using Application.Features.BootcampContents.Rules;
using Application.Features.Bootcamps.Queries.GetList;
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

namespace Application.Features.BootcampContents.Queries;
public class GetBootcampContentByBootcampIdQuery : IRequest<GetListResponse<GetListBootcampContentListItemDto>> //, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public int BootcampId { get; set; }
    public Guid? ApplicantId { get; set; }
  
    //public string[] Roles => [Admin, Read];

    //public bool BypassCache { get; }
    //public string? CacheKey => $"GetListBootcamps({PageRequest.PageIndex},{PageRequest.PageSize})";
    //public string? CacheGroupKey => "GetBootcamps";
    //public TimeSpan? SlidingExpiration { get; }

    public class GetBootcampContentByBootcampIdQueryHandler
        : IRequestHandler<GetBootcampContentByBootcampIdQuery, GetListResponse<GetListBootcampContentListItemDto>>
    {
        private readonly IBootcampContentRepository _bootcampContentRepository;
        private readonly IMapper _mapper;
        private readonly BootcampContentBusinessRules _bootcampContentBusinessRules;

        public GetBootcampContentByBootcampIdQueryHandler(IBootcampContentRepository bootcampContentRepository, 
            IMapper mapper, BootcampContentBusinessRules bootcampContentBusinessRules)
        {
            _bootcampContentRepository = bootcampContentRepository;
            _mapper = mapper;
            _bootcampContentBusinessRules = bootcampContentBusinessRules;
        }

        public async Task<GetListResponse<GetListBootcampContentListItemDto>> Handle(
            GetBootcampContentByBootcampIdQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<BootcampContent> bootcampContents = await _bootcampContentRepository.GetListAsync(
                predicate: x => x.BootcampId == request.BootcampId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: b => b.Include(b => b.Bootcamp)
            );

           

            GetListResponse<GetListBootcampContentListItemDto> response = _mapper.Map<GetListResponse<GetListBootcampContentListItemDto>>(
                bootcampContents,
                 options => options.AfterMap((source, destination) => destination.Items.ToList().ForEach(
                 item => item.HasApplicantBootcampContent =
                    _bootcampContentBusinessRules.HasApplicantBootcampContent(request.ApplicantId, item.Id)))
            );

            return response;

        }
    }
}
