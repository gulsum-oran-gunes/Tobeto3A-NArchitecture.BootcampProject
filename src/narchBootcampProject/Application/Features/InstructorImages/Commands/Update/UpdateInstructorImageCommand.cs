using Application.Features.InstructorImages.Constants;
using Application.Features.InstructorImages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.InstructorImages.Constants.InstructorImagesOperationClaims;

namespace Application.Features.InstructorImages.Commands.Update;

public class UpdateInstructorImageCommand : IRequest<UpdatedInstructorImageResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public Guid InstructorId { get; set; }
    public string ImagePath { get; set; }

    public string[] Roles => [Admin, Write, InstructorImagesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetInstructorImages"];

    public class UpdateInstructorImageCommandHandler : IRequestHandler<UpdateInstructorImageCommand, UpdatedInstructorImageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IInstructorImageRepository _instructorImageRepository;
        private readonly InstructorImageBusinessRules _instructorImageBusinessRules;

        public UpdateInstructorImageCommandHandler(IMapper mapper, IInstructorImageRepository instructorImageRepository,
                                         InstructorImageBusinessRules instructorImageBusinessRules)
        {
            _mapper = mapper;
            _instructorImageRepository = instructorImageRepository;
            _instructorImageBusinessRules = instructorImageBusinessRules;
        }

        public async Task<UpdatedInstructorImageResponse> Handle(UpdateInstructorImageCommand request, CancellationToken cancellationToken)
        {
            InstructorImage? instructorImage = await _instructorImageRepository.GetAsync(predicate: ii => ii.Id == request.Id, cancellationToken: cancellationToken);
            await _instructorImageBusinessRules.InstructorImageShouldExistWhenSelected(instructorImage);
            instructorImage = _mapper.Map(request, instructorImage);

            await _instructorImageRepository.UpdateAsync(instructorImage!);

            UpdatedInstructorImageResponse response = _mapper.Map<UpdatedInstructorImageResponse>(instructorImage);
            return response;
        }
    }
}