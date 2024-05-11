using System.Linq.Expressions;
using Application.Features.BootcampImages.Rules;
using Application.Features.Bootcamps.Commands.Update;
using Application.Services.ImageService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Persistence.Paging;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Application.Services.BootcampImages;

public class BootcampImageManager : IBootcampImageService, ICacheRemoverRequest
{


    private readonly IBootcampImageRepository _bootcampImageRepository;
    private readonly BootcampImageBusinessRules _bootcampImageBusinessRules;
    private readonly ImageServiceBase _imageService;
    private readonly IMapper _mapper;

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBootcampImages"];

    public BootcampImageManager(
        IBootcampImageRepository bootcampImageRepository,
        BootcampImageBusinessRules bootcampImageBusinessRules,
        ImageServiceBase imageService,
        IMapper mapper

    )
    {
        _bootcampImageRepository = bootcampImageRepository;
        _bootcampImageBusinessRules = bootcampImageBusinessRules;
        _imageService = imageService;
        _mapper = mapper;
    }

    public async Task<List<BootcampImage>> GetList()
    {
        throw new NotImplementedException();
    }

    public Task<BootcampImage> Get(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<BootcampImage> Add(IFormFile file, BootcampImageRequest request)
    {
        BootcampImage bootcampImage = new BootcampImage() { BootcampId = request.BootcampId, ImagePath = request.ImagePath, };
        bootcampImage.ImagePath = await _imageService.UploadAsync(file);
        return await _bootcampImageRepository.AddAsync(bootcampImage);
    }

    public async Task<BootcampImage> Update(IFormFile file, UpdateBootcampImageRequest request)
    {
        BootcampImage bootcampImage = await _bootcampImageRepository.GetAsync(x => x.Id == request.Id);
        bootcampImage = _mapper.Map(request, bootcampImage);
        bootcampImage.ImagePath = await _imageService.UpdateAsync(file, bootcampImage.ImagePath);
        await _bootcampImageRepository.UpdateAsync(bootcampImage);
        return bootcampImage;
    }

    public Task<BootcampImage> Delete(BootcampImage BootcampImage)
    {
        throw new NotImplementedException();
    }

    public Task<List<BootcampImage>> GetImagesByBootcampId(Guid id)
    {
        throw new NotImplementedException();
    }
}
