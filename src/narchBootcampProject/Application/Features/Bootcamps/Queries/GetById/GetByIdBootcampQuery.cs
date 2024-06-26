using Application.Features.ApplicationEntities.Rules;
using Application.Features.Bootcamps.Constants;
using Application.Features.Bootcamps.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Bootcamps.Constants.BootcampsOperationClaims;

namespace Application.Features.Bootcamps.Queries.GetById;

public class GetByIdBootcampQuery : IRequest<GetByIdBootcampResponse>/*, ISecuredRequest*/
{
    public int Id { get; set; }
    public Guid? ApplicantId { get; set; }

    //public string[] Roles => [Admin, Read];

    public class GetByIdBootcampQueryHandler : IRequestHandler<GetByIdBootcampQuery, GetByIdBootcampResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBootcampRepository _bootcampRepository;
        private readonly BootcampBusinessRules _bootcampBusinessRules;
        private readonly ApplicationEntityBusinessRules _applicationEntityBusinessRules;

        public GetByIdBootcampQueryHandler(
            IMapper mapper,
            IBootcampRepository bootcampRepository,
            BootcampBusinessRules bootcampBusinessRules,
            ApplicationEntityBusinessRules applicationEntityBusinessRules
        )
        {
            _mapper = mapper;
            _bootcampRepository = bootcampRepository;
            _bootcampBusinessRules = bootcampBusinessRules;
            _applicationEntityBusinessRules = applicationEntityBusinessRules;
        }

        public async Task<GetByIdBootcampResponse> Handle(GetByIdBootcampQuery request, CancellationToken cancellationToken)
        {
            Bootcamp? bootcamp = await _bootcampRepository.GetAsync(
                predicate: b => b.Id == request.Id,
                cancellationToken: cancellationToken,
                 include: x => x.Include(p => p.Instructor).ThenInclude(p => p.InstructorImages).Include(p => p.BootcampState).Include(p => p.BootcampImages)
            );
            await _bootcampBusinessRules.BootcampShouldExistWhenSelected(bootcamp);
         
            GetByIdBootcampResponse response = _mapper.Map<GetByIdBootcampResponse>(bootcamp);
            response.IfApplicantApplied = _applicationEntityBusinessRules.IfApplicantApplied(request.Id, request.ApplicantId);
            return response; 
            
         
           
        }


    }
}
