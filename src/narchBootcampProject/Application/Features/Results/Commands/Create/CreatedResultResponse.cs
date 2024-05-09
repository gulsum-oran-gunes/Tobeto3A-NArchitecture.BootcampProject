using NArchitecture.Core.Application.Responses;

namespace Application.Features.Results.Commands.Create;

public class CreatedResultResponse : IResponse
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int QuizId { get; set; }
    public int WrongAnswers { get; set; }
    public int CorrectAnswers { get; set; }
    public bool IsPassed {  get; set; }
    public int Score => CorrectAnswers * 20;

}
