using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Questions.Queries.GetList;

public class GetListQuestionListItemDto : IDto
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
