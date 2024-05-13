using Application.Features.InstructorImages.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.AspNetCore.Http;
using Application.Services.ImageService;
using AutoMapper;
using Application.Features.BootcampImages.Commands.Delete;
using Application.Features.InstructorImages.Commands.Delete;
using NArchitecture.Core.Application.Pipelines.Caching;

namespace Application.Services.InstructorImages;

public class InstructorImageManager : IInstructorImageService, ICacheRemoverRequest
{
    private readonly IInstructorImageRepository _instructorImageRepository;
    private readonly InstructorImageBusinessRules _instructorImageBusinessRules;
    private readonly ImageServiceBase _imageService;
    private readonly IMapper _mapper;
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetInstructorImages"];
    public InstructorImageManager(IInstructorImageRepository instructorImageRepository, InstructorImageBusinessRules instructorImageBusinessRules,
        ImageServiceBase imageServiceBase, IMapper mapper)
    {
        _instructorImageRepository = instructorImageRepository;
        _instructorImageBusinessRules = instructorImageBusinessRules;
        _imageService = imageServiceBase;
        _mapper = mapper;
    }

    public async Task<InstructorImage?> GetAsync(
        Expression<Func<InstructorImage, bool>> predicate,
        Func<IQueryable<InstructorImage>, IIncludableQueryable<InstructorImage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        InstructorImage? instructorImage = await _instructorImageRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return instructorImage;
    }

    public async Task<IPaginate<InstructorImage>?> GetListAsync(
        Expression<Func<InstructorImage, bool>>? predicate = null,
        Func<IQueryable<InstructorImage>, IOrderedQueryable<InstructorImage>>? orderBy = null,
        Func<IQueryable<InstructorImage>, IIncludableQueryable<InstructorImage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<InstructorImage> instructorImageList = await _instructorImageRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return instructorImageList;
    }

    public async Task<InstructorImage> AddAsync(IFormFile file, InstructorImageRequest request)
    {
        InstructorImage instructorImage = new InstructorImage() { InstructorId = request.InstructorId };
        instructorImage.ImagePath = await _imageService.UploadAsync(file);
        return await _instructorImageRepository.AddAsync(instructorImage);

    }

    public async Task<InstructorImage> UpdateAsync(IFormFile file, UpdateInstructorImageRequest request)
    {
        InstructorImage instructorImage = await _instructorImageRepository.GetAsync(x => x.Id == request.Id);
        instructorImage = _mapper.Map(request, instructorImage);
        instructorImage.ImagePath = await _imageService.UpdateAsync(file, instructorImage.ImagePath);
        await _instructorImageRepository.UpdateAsync(instructorImage);
        return instructorImage;
    }

    public async Task<DeletedInstructorImageResponse> DeleteAsync(int id)
    {
        InstructorImage instructorImage = await _instructorImageRepository.GetAsync(x => x.Id == id);
        await _imageService.DeleteAsync(instructorImage.ImagePath);
        await _instructorImageRepository.DeleteAsync(instructorImage, true);

        DeletedInstructorImageResponse response = _mapper.Map<DeletedInstructorImageResponse>(instructorImage);
        return response;

    }
}
