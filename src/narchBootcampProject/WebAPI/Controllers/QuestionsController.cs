using Application.Features.Questions.Commands.Create;
using Application.Features.Questions.Commands.Delete;
using Application.Features.Questions.Commands.Update;
using Application.Features.Questions.Queries.GetById;
using Application.Features.Questions.Queries.GetList;
using Application.Services.Questions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionsController : BaseController
{
    private readonly IQuestionService _questionService;

    public QuestionsController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateQuestionCommand createQuestionCommand)
    {
        CreatedQuestionResponse response = await Mediator.Send(createQuestionCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateQuestionCommand updateQuestionCommand)
    {
        UpdatedQuestionResponse response = await Mediator.Send(updateQuestionCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedQuestionResponse response = await Mediator.Send(new DeleteQuestionCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdQuestionResponse response = await Mediator.Send(new GetByIdQuestionQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListQuestionQuery getListQuestionQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListQuestionListItemDto> response = await Mediator.Send(getListQuestionQuery);
        return Ok(response);
    }

    [HttpGet("random/{bootcampId}")]
    public async Task<List<Question>> GetRandomQuestionsByBootcampIdAsync([FromRoute] int bootcampId)
    {
        return await _questionService.GetRandomQuestionsByBootcampIdAsync(bootcampId);
    }
}
