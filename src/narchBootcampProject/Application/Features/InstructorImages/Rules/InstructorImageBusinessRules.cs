using Application.Features.InstructorImages.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.InstructorImages.Rules;

public class InstructorImageBusinessRules : BaseBusinessRules
{
    private readonly IInstructorImageRepository _instructorImageRepository;
    private readonly ILocalizationService _localizationService;

    public InstructorImageBusinessRules(IInstructorImageRepository instructorImageRepository, ILocalizationService localizationService)
    {
        _instructorImageRepository = instructorImageRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, InstructorImagesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task InstructorImageShouldExistWhenSelected(InstructorImage? instructorImage)
    {
        if (instructorImage == null)
            await throwBusinessException(InstructorImagesBusinessMessages.InstructorImageNotExists);
    }

    public async Task InstructorImageIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        InstructorImage? instructorImage = await _instructorImageRepository.GetAsync(
            predicate: ii => ii.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await InstructorImageShouldExistWhenSelected(instructorImage);
    }
}