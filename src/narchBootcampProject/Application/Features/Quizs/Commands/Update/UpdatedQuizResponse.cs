using NArchitecture.Core.Application.Responses;

namespace Application.Features.Quizs.Commands.Update;

public class UpdatedQuizResponse : IResponse
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int BootcampId { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
