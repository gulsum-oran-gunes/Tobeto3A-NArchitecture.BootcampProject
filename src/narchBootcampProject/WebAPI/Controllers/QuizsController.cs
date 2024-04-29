using Application.Features.Quizs.Commands.Create;
using Application.Features.Quizs.Commands.Delete;
using Application.Features.Quizs.Commands.Finish;
using Application.Features.Quizs.Commands.Update;
using Application.Features.Quizs.Queries.GetById;
using Application.Features.Quizs.Queries.GetList;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuizsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateQuizCommand createQuizCommand)
    {
        CreatedQuizResponse response = await Mediator.Send(createQuizCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateQuizCommand updateQuizCommand)
    {
        UpdatedQuizResponse response = await Mediator.Send(updateQuizCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedQuizResponse response = await Mediator.Send(new DeleteQuizCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdQuizResponse response = await Mediator.Send(new GetByIdQuizQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListQuizQuery getListQuizQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListQuizListItemDto> response = await Mediator.Send(getListQuizQuery);
        return Ok(response);
    }

    [HttpPost("Finish")]
    public async Task<IActionResult> Finish([FromBody] FinishQuizCommand finishCommand)
    {
        FinishedQuizResponse response = await Mediator.Send(finishCommand);
        return Ok(response);
    }
}
