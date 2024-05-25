using Application.Features.ApplicationEntities.Constants;
using Application.Features.ApplicationEntities.Queries.GetList;
using Application.Features.Bootcamps.Constants;
using Application.Features.Bootcamps.Queries.GetList;
using Application.Features.Bootcamps.Queries.GetListDynamic;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ApplicationEntities.Queries.GetListDynamic;
public class GetListApplicationDynamicQuery: IRequest<GetListResponse<GetListApplicationEntityListItemDto>>/*, ISecuredRequest*/
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery Dynamic { get; set; }

    //public string[] Roles => [ApplicationEntitiesOperationClaims.Admin, ApplicationEntitiesOperationClaims.Write, ApplicationEntitiesOperationClaims.Read];

    //public bool BypassCache { get; }
    //public string? CacheKey => $"GetListBootcampsDynamic({PageRequest.PageIndex},{PageRequest.PageSize})";
    //public string? CacheGroupKey => "GetBootcampsDynamic";
    //public TimeSpan? SlidingExpiration { get; }

    public class GetListApplicationDynamicQueryHandler : IRequestHandler<GetListApplicationDynamicQuery, GetListResponse<GetListApplicationEntityListItemDto>>
    {
        private readonly IApplicationEntityRepository _applicationRepository;
        private readonly IMapper _mapper;

        public GetListApplicationDynamicQueryHandler(IApplicationEntityRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListApplicationEntityListItemDto>> Handle(
            GetListApplicationDynamicQuery request,
            CancellationToken cancellationToken
        )

        {
            IPaginate<ApplicationEntity> applications = await _applicationRepository.GetListByDynamicAsync(
                
                request.Dynamic,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: p => p.Include(x => x.Applicant).Include(x => x.ApplicationState)
                .Include(x => x.Bootcamp).ThenInclude(x => x.BootcampImages)
                .Include(x => x.Bootcamp).ThenInclude(x => x.Instructor)
                .Include(x => x.Bootcamp).ThenInclude(x => x.BootcampState)
                
            
                
                );

           
            GetListResponse<GetListApplicationEntityListItemDto> response = _mapper.Map<GetListResponse<GetListApplicationEntityListItemDto>>(applications);
            return response;


        }
    }
}
