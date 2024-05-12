using Application.Features.BootcampImages.Commands.Create;
using Application.Features.BootcampImages.Commands.Delete;
using Application.Features.BootcampImages.Commands.Update;
using Application.Features.BootcampImages.Queries.GetById;
using Application.Features.BootcampImages.Queries.GetList;
using Application.Services.BootcampImages;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BootcampImagesController : BaseController
{
    private readonly IBootcampImageService _bootcampImageService;

    public BootcampImagesController(IBootcampImageService bootcampImageService)
    {
        _bootcampImageService = bootcampImageService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(IFormFile file, [FromForm] BootcampImageRequest request)
    {
        var result = await _bootcampImageService.Add(file, request);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(IFormFile file, [FromForm] UpdateBootcampImageRequest updateRequest)
    {
        var result = await _bootcampImageService.Update(file, updateRequest);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _bootcampImageService.Delete(id);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdBootcampImageResponse response = await Mediator.Send(new GetByIdBootcampImageQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBootcampImageQuery getListBootcampImageQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListBootcampImageListItemDto> response = await Mediator.Send(getListBootcampImageQuery);
        return Ok(response);
    }
}
