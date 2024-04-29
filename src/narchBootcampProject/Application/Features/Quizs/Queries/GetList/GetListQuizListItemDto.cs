using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Quizs.Queries.GetList;

public class GetListQuizListItemDto : IDto
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int BootcampId { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
