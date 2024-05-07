using NArchitecture.Core.Application.Dtos;

namespace Application.Features.QuizQuestions.Queries.GetList;

public class GetListQuizQuestionListItemDto : IDto
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public string QuestionText { get; set; }
    public string QuestionAnswerA { get; set; }
    public string QuestionAnswerB { get; set; }
    public string QuestionAnswerC { get; set; }
    public string QuestionAnswerD { get; set; }
    public string QuestionCorrectAnswer { get; set; }
}
