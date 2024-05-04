using Application.Features.BootcampImages.Rules;
using Application.Features.BootcampVideos.Rules;
using Application.Services.BootcampImages;
using Application.Services.ImageService;
using Application.Services.Repositories;
using Application.Services.VideoService;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BootcampVideoService;
public class BootcampVideoManager : IBootcampVideoService
{
    private readonly IBootcampVideoRepository _bootcampVideoRepository;
    private readonly BootcampVideoBusinessRules _bootcampVideoBusinessRules;
    private readonly VideoServiceBase _videoService;

    public BootcampVideoManager(
        IBootcampVideoRepository bootcampVideoRepository,
        BootcampVideoBusinessRules bootcampVideoBusinessRules,
        VideoServiceBase videoService
    )
    {
        _bootcampVideoRepository = bootcampVideoRepository;
        _bootcampVideoBusinessRules = bootcampVideoBusinessRules;
        _videoService = videoService;
    }

    public async Task<List<BootcampVideo>> GetList()
    {
        throw new NotImplementedException();
    }

    public Task<BootcampVideo> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<BootcampVideo> Add(IFormFile videoFile, BootcampVideoRequest request)
    {
        BootcampVideo bootcampVideo = new BootcampVideo() { BootcampId = request.BootcampId, ThumbnailUrl = request.ThumbnailUrl, };
        bootcampVideo.ThumbnailUrl = await _videoService.UploadAsync(videoFile);
        return await _bootcampVideoRepository.AddAsync(bootcampVideo);
    }

    public Task<BootcampVideo> Update(IFormFile file, BootcampVideo BootcampVideo)
    {
        throw new NotImplementedException();
    }

    public Task<BootcampVideo> Delete(BootcampVideo BootcampVideo)
    {
        throw new NotImplementedException();
    }

    public Task<List<BootcampVideo>> GetVideosByBootcampId(Guid id)
    {
        throw new NotImplementedException();
    }
}

    
