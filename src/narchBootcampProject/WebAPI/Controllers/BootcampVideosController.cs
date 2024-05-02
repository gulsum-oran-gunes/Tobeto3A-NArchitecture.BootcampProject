using Application.Features.BootcampImages.Commands.Delete;
using Application.Features.BootcampImages.Commands.Update;
using Application.Features.BootcampImages.Queries.GetById;
using Application.Features.BootcampImages.Queries.GetList;
using Application.Services.BootcampImages;
using Application.Services.BootcampVideoService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BootcampVideosController : BaseController
{
    private readonly IBootcampVideoService _bootcampVideoService;

    public BootcampVideosController(IBootcampVideoService bootcampVideoService)
    {
        _bootcampVideoService = bootcampVideoService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Add(IFormFile videoFile, BootcampVideoRequest request)
    {
        if (videoFile == null || videoFile.Length == 0)
        {
            return BadRequest("Video dosyası seçilmedi.");
        }

        // Video yükleme servisini kullanarak videoyu yükleyin
        var videoPath = await _bootcampVideoService.Add(videoFile, request);

        if (videoPath == null)
        {
            return BadRequest("Video yüklenirken bir hata oluştu.");
        }

        // Başarı durumunda video yolunu döndürün
        return Ok(new { VideoPath = videoPath });
    }
}

