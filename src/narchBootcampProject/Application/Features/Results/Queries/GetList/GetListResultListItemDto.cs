using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Results.Queries.GetList;

public class GetListResultListItemDto : IDto
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public int WrongAnswers { get; set; }
    public int CorrectAnswers { get; set; }
}
