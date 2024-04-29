using NArchitecture.Core.Application.Responses;

namespace Application.Features.Results.Commands.Update;

public class UpdatedResultResponse : IResponse
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public int WrongAnswers { get; set; }
    public int CorrectAnswers { get; set; }
}
