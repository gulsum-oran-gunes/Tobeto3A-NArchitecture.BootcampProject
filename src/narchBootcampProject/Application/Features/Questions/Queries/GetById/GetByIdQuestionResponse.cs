using NArchitecture.Core.Application.Responses;

namespace Application.Features.Questions.Queries.GetById;

public class GetByIdQuestionResponse : IResponse
{
    public int Id { get; set; }
    public int BootcampId { get; set; }
    public string Text { get; set; }
    public string AnswerA { get; set; }
    public string AnswerB { get; set; }
    public string AnswerC { get; set; }
    public string AnswerD { get; set; }
    public string CorrectAnswer { get; set; }
}
