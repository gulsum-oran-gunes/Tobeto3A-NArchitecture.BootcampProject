using Application.Features.ApplicationEntities.Commands.Create;
using Application.Features.ApplicationEntities.Commands.Delete;
using Application.Features.ApplicationEntities.Commands.Update;
using Application.Features.ApplicationEntities.Queries.GetById;
using Application.Features.ApplicationEntities.Queries.GetList;
using Application.Features.ApplicationEntities.Queries.GetListDynamic;
using Application.Features.Bootcamps.Queries.GetList;
using Application.Features.Bootcamps.Queries.GetListDynamic;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationEntitiesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateApplicationEntityCommand createApplicationEntityCommand)
    {
        CreatedApplicationEntityResponse response = await Mediator.Send(createApplicationEntityCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateApplicationEntityCommand updateApplicationEntityCommand)
    {
        UpdatedApplicationEntityResponse response = await Mediator.Send(updateApplicationEntityCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedApplicationEntityResponse response = await Mediator.Send(new DeleteApplicationEntityCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdApplicationEntityResponse response = await Mediator.Send(new GetByIdApplicationEntityQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListApplicationEntityQuery getListApplicationEntityQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListApplicationEntityListItemDto> response = await Mediator.Send(getListApplicationEntityQuery);
        return Ok(response);
    }

    [HttpPost("dynamic")]
    public async Task<IActionResult> GetListDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery dynamic)
    {
        GetListApplicationDynamicQuery applicationDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
        GetListResponse<GetListApplicationEntityListItemDto> response = await Mediator.Send(applicationDynamicQuery);
        return Ok(response);

    }
}
