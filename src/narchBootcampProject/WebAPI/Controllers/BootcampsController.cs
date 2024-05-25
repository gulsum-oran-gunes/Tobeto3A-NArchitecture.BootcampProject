using Application.Features.BootcampContents.Queries.GetList;
using Application.Features.Bootcamps.Commands.Create;
using Application.Features.Bootcamps.Commands.Delete;
using Application.Features.Bootcamps.Commands.Update;
using Application.Features.Bootcamps.Queries.GetById;
using Application.Features.Bootcamps.Queries.GetList;
using Application.Features.Bootcamps.Queries.GetListByInstructorId;
using Application.Features.Bootcamps.Queries.GetListDynamic;
using Azure;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using static Google.Apis.YouTube.v3.VideosResource;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BootcampsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBootcampCommand createBootcampCommand)
    {
        CreatedBootcampResponse response = await Mediator.Send(createBootcampCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBootcampCommand updateBootcampCommand)
    {
        UpdatedBootcampResponse response = await Mediator.Send(updateBootcampCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedBootcampResponse response = await Mediator.Send(new DeleteBootcampCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id, Guid? applicantId)
    {
        GetByIdBootcampQuery getByIdBootcampQuery = new() { Id=id, ApplicantId = applicantId };
        GetByIdBootcampResponse response = await Mediator.Send(getByIdBootcampQuery);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBootcampQuery getListBootcampQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListBootcampListItemDto> response = await Mediator.Send(getListBootcampQuery);
        return Ok(response);
    }
    [HttpGet("getbootcampbyinstructorid")]
    public async Task<IActionResult> GetListBootcampByInstructorId([FromQuery] PageRequest pageRequest, Guid instructorId)
    {
        GetListBootcampByInstructorIdQuery query = new() { PageRequest = pageRequest, InstructorId = instructorId };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("dynamic")]
    public async Task<IActionResult> GetListDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery dynamic)
    {
        GetListBootcampDynamicQuery bootcampDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
        GetListResponse<GetListBootcampListItemDto> response = await Mediator.Send(bootcampDynamicQuery);
        return Ok(response);

    }



}
