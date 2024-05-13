using Application.Features.InstructorImages.Commands.Create;
using Application.Features.InstructorImages.Commands.Delete;
using Application.Features.InstructorImages.Commands.Update;
using Application.Features.InstructorImages.Queries.GetById;
using Application.Features.InstructorImages.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Services.InstructorImages;
using Application.Services.BootcampImages;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstructorImagesController : BaseController


{

    private readonly IInstructorImageService _instructorImageService;

    public InstructorImagesController(IInstructorImageService instructorImageService)
    {
        _instructorImageService = instructorImageService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(IFormFile file, [FromForm] InstructorImageRequest request)
    {
        var result = await _instructorImageService.AddAsync(file, request);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(IFormFile file, [FromForm] UpdateInstructorImageRequest updateRequest)
    {
        var result = await _instructorImageService.UpdateAsync(file, updateRequest);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _instructorImageService.DeleteAsync(id);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdInstructorImageResponse response = await Mediator.Send(new GetByIdInstructorImageQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListInstructorImageQuery getListInstructorImageQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListInstructorImageListItemDto> response = await Mediator.Send(getListInstructorImageQuery);
        return Ok(response);
    }
}