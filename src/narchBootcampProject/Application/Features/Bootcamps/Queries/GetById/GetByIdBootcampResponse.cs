using NArchitecture.Core.Application.Responses;

namespace Application.Features.Bootcamps.Queries.GetById;

public class GetByIdBootcampResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid InstructorId { get; set; }
    public string InstructorFirstName { get; set; }
    public string InstructorLastName { get; set; }
    public int InstructorImageId { get; set; }
    public string InstructorImagePath { get; set; }
    public int BootcampStateId { get; set; }
    public string BootcampStateName { get; set; }
    public int BootcampImageId { get; set; }
    public string BootcampImagePath { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Detail { get; set; }
    public DateTime Deadline { get; set; }
    public bool? IfApplicantApplied { get; set; }
}
