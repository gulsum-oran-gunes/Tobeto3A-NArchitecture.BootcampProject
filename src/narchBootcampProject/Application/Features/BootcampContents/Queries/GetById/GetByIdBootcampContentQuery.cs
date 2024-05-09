using Application.Features.BootcampContents.Constants;
using Application.Features.BootcampContents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.BootcampContents.Constants.BootcampContentsOperationClaims;

namespace Application.Features.BootcampContents.Queries.GetById;

public class GetByIdBootcampContentQuery : IRequest<GetByIdBootcampContentResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdBootcampContentQueryHandler : IRequestHandler<GetByIdBootcampContentQuery, GetByIdBootcampContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBootcampContentRepository _bootcampContentRepository;
        private readonly BootcampContentBusinessRules _bootcampContentBusinessRules;

        public GetByIdBootcampContentQueryHandler(IMapper mapper, IBootcampContentRepository bootcampContentRepository, BootcampContentBusinessRules bootcampContentBusinessRules)
        {
            _mapper = mapper;
            _bootcampContentRepository = bootcampContentRepository;
            _bootcampContentBusinessRules = bootcampContentBusinessRules;
        }

        public async Task<GetByIdBootcampContentResponse> Handle(GetByIdBootcampContentQuery request, CancellationToken cancellationToken)
        {
            BootcampContent? bootcampContent = await _bootcampContentRepository.GetAsync(predicate: bc => bc.Id == request.Id, cancellationToken: cancellationToken);
            await _bootcampContentBusinessRules.BootcampContentShouldExistWhenSelected(bootcampContent);

            GetByIdBootcampContentResponse response = _mapper.Map<GetByIdBootcampContentResponse>(bootcampContent);
            return response;
        }
    }
}