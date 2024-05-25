using AutoMapper.Configuration.Annotations;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Quizs.Commands.Create;

public class CreatedQuizResponse : IResponse
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int BootcampId { get; set; }
    public string BootcampName { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public List<QuestionResponse> QuestionResponses { get; set; }
}
