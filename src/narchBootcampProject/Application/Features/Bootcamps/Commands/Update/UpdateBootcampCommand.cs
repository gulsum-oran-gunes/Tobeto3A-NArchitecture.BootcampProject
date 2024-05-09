using Application.Features.Bootcamps.Constants;
using Application.Features.Bootcamps.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.Bootcamps.Constants.BootcampsOperationClaims;

namespace Application.Features.Bootcamps.Commands.Update;

public class UpdateBootcampCommand
    : IRequest<UpdatedBootcampResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid InstructorId { get; set; }
    public int BootcampStateId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Detail { get; set; }
    public DateTime Deadline { get; set; }

    public string[] Roles => [Admin, Write, BootcampsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBootcamps"];

    public class UpdateBootcampCommandHandler : IRequestHandler<UpdateBootcampCommand, UpdatedBootcampResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBootcampRepository _bootcampRepository;
        private readonly BootcampBusinessRules _bootcampBusinessRules;

        public UpdateBootcampCommandHandler(
            IMapper mapper,
            IBootcampRepository bootcampRepository,
            BootcampBusinessRules bootcampBusinessRules
        )
        {
            _mapper = mapper;
            _bootcampRepository = bootcampRepository;
            _bootcampBusinessRules = bootcampBusinessRules;
        }

        public async Task<UpdatedBootcampResponse> Handle(UpdateBootcampCommand request, CancellationToken cancellationToken)
        {
            Bootcamp? bootcamp = await _bootcampRepository.GetAsync(
                predicate: b => b.Id == request.Id,
                cancellationToken: cancellationToken,
                include: p => p.Include(x => x.Instructor).Include(p => p.BootcampState).Include(p => p.BootcampImages)
            );
            await _bootcampBusinessRules.BootcampShouldExistWhenSelected(bootcamp);
            bootcamp = _mapper.Map(request, bootcamp);

            await _bootcampRepository.UpdateAsync(bootcamp!);

            UpdatedBootcampResponse response = _mapper.Map<UpdatedBootcampResponse>(bootcamp);
            return response;
        }
    }
}
