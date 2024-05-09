using Application.Features.BootcampContents.Commands.Create;
using Application.Features.BootcampContents.Commands.Delete;
using Application.Features.BootcampContents.Commands.Update;
using Application.Features.BootcampContents.Queries.GetById;
using Application.Features.BootcampContents.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BootcampContentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBootcampContentCommand createBootcampContentCommand)
    {
        CreatedBootcampContentResponse response = await Mediator.Send(createBootcampContentCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBootcampContentCommand updateBootcampContentCommand)
    {
        UpdatedBootcampContentResponse response = await Mediator.Send(updateBootcampContentCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedBootcampContentResponse response = await Mediator.Send(new DeleteBootcampContentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdBootcampContentResponse response = await Mediator.Send(new GetByIdBootcampContentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBootcampContentQuery getListBootcampContentQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListBootcampContentListItemDto> response = await Mediator.Send(getListBootcampContentQuery);
        return Ok(response);
    }
}