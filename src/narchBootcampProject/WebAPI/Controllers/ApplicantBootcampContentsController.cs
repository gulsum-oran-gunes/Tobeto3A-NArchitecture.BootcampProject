using Application.Features.ApplicantBootcampContents.Commands.Create;
using Application.Features.ApplicantBootcampContents.Commands.Delete;
using Application.Features.ApplicantBootcampContents.Commands.Update;
using Application.Features.ApplicantBootcampContents.Queries.GetById;
using Application.Features.ApplicantBootcampContents.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicantBootcampContentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateApplicantBootcampContentCommand createApplicantBootcampContentCommand)
    {
        CreatedApplicantBootcampContentResponse response = await Mediator.Send(createApplicantBootcampContentCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateApplicantBootcampContentCommand updateApplicantBootcampContentCommand)
    {
        UpdatedApplicantBootcampContentResponse response = await Mediator.Send(updateApplicantBootcampContentCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedApplicantBootcampContentResponse response = await Mediator.Send(new DeleteApplicantBootcampContentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdApplicantBootcampContentResponse response = await Mediator.Send(new GetByIdApplicantBootcampContentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListApplicantBootcampContentQuery getListApplicantBootcampContentQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListApplicantBootcampContentListItemDto> response = await Mediator.Send(getListApplicantBootcampContentQuery);
        return Ok(response);
    }
}