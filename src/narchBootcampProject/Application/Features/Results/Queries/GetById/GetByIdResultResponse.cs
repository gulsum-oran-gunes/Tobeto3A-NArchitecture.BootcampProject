using NArchitecture.Core.Application.Responses;

namespace Application.Features.Results.Queries.GetById;

public class GetByIdResultResponse : IResponse
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public Guid ApplicantId { get; set; }
    public string ApplicantFirstName { get; set; }
    public string ApplicantLastName { get; set; }
    public int BootcampId { get; set; }
    public string BootcampName { get; set; }
    public int WrongAnswers { get; set; }
    public int CorrectAnswers { get; set; }
}
