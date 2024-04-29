using Application.Features.Results.Commands.Create;
using Application.Features.Results.Commands.Delete;
using Application.Features.Results.Commands.Update;
using Application.Features.Results.Queries.GetById;
using Application.Features.Results.Queries.GetList;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResultsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateResultCommand createResultCommand)
    {
        CreatedResultResponse response = await Mediator.Send(createResultCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateResultCommand updateResultCommand)
    {
        UpdatedResultResponse response = await Mediator.Send(updateResultCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedResultResponse response = await Mediator.Send(new DeleteResultCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdResultResponse response = await Mediator.Send(new GetByIdResultQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListResultQuery getListResultQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListResultListItemDto> response = await Mediator.Send(getListResultQuery);
        return Ok(response);
    }
}
