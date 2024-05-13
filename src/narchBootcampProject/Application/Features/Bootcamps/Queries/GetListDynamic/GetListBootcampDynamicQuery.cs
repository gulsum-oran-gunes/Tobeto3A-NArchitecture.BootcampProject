﻿using Application.Features.Bootcamps.Constants;
using Application.Features.Bootcamps.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.Paging;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Bootcamps.Queries.GetListDynamic;
public class GetListBootcampDynamicQuery: IRequest<GetListResponse<GetListBootcampListItemDto>>, ISecuredRequest/*, ICachableRequest*/

{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery Dynamic { get; set; }

    public string[] Roles => [ BootcampsOperationClaims.Admin, BootcampsOperationClaims.Write, BootcampsOperationClaims.Read];

    //public bool BypassCache { get; }
    //public string? CacheKey => $"GetListBootcampsDynamic({PageRequest.PageIndex},{PageRequest.PageSize})";
    //public string? CacheGroupKey => "GetBootcampsDynamic";
    //public TimeSpan? SlidingExpiration { get; }

    public class GetListBootcampDynamicQueryHandler : IRequestHandler<GetListBootcampDynamicQuery, GetListResponse<GetListBootcampListItemDto>>
    {
        private readonly IBootcampRepository _bootcampRepository;
        private readonly IMapper _mapper;

        public GetListBootcampDynamicQueryHandler(IBootcampRepository bootcampRepository, IMapper mapper)
        {
            _bootcampRepository = bootcampRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBootcampListItemDto>> Handle(
            GetListBootcampDynamicQuery request,
            CancellationToken cancellationToken
        )

        {
            IPaginate<Bootcamp> bootcamps = await _bootcampRepository.GetListByDynamicAsync(
                request.Dynamic,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: p => p.Include(x => x.Instructor).Include(p => p.BootcampState).Include(p => p.BootcampImages)
            );

            GetListResponse<GetListBootcampListItemDto> response = _mapper.Map<GetListResponse<GetListBootcampListItemDto>>( bootcamps);
           return response;
        }
    }
}

