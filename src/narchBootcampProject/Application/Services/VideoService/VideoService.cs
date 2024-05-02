using Google.Apis.Services;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Http;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services.VideoService;
public abstract class VideoServiceBase
{
    public abstract Task<string> UploadAsync(IFormFile videoFile);

    public async Task<string> UpdateAsync(IFormFile videoFile, string thumbnailUrl)
    {
        await FileMustBeInVideoFormat(videoFile);

        await DeleteAsync(thumbnailUrl);
        return await UploadAsync(videoFile);
    }
    /*BootcampVideo video = new BootcampVideo()
    {
        Id = 1,
        BootcampId = 123,
        Title = "Sample Bootcamp Video",
        Description = "This is a sample bootcamp video description.",
        ThumbnailUrl = "https://support.google.com/youtube/answer/171780?hl=tr",
    }; */

    public abstract Task DeleteAsync(string thumbnailUrl);

    protected async Task FileMustBeInVideoFormat(IFormFile videoFile)
    {
        List<string> videoExtensions = new List<string> { ".mp4", ".avi", ".mkv", ".mov", ".wmv", ".webm", ".ogg" };

        string extension = Path.GetExtension(videoFile.FileName).ToLower();
        if (!videoExtensions.Contains(extension))
        {
            throw new BusinessException("Video formatı desteklenmiyor.");
        }
        await Task.CompletedTask;
    }
    /*public void FetchVideoDetails()
    {
        var youtubeService = new YouTubeService(new BaseClientService.Initializer()
        {
            ApiKey = "YOURAPIKEY_HERE",
            ApplicationName = "YourApplicationName"
        });
        
        var videoRequest = youtubeService.Videos.List("snippet");
        videoRequest.Id = videoId;

        var response = videoRequest.Execute();

        var video = response.Items.FirstOrDefault();

        if (video != null)
        {
            Title = video.Snippet.Title;
            Description = video.Snippet.Description;
            ThumbnailUrl = video.Snippet.Thumbnails.Default.Url;
        }
    } */

}

