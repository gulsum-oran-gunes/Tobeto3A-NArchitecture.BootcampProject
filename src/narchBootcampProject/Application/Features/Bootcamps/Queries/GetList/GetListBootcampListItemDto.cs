using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Bootcamps.Queries.GetList;

public class GetListBootcampListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid InstructorId { get; set; }
    public string InstructorFirstName { get; set; }
    public string InstructorLastName { get; set; }
    public int BootcampStateId { get; set; }
    public string BootcampStateName { get; set; }
    public int BootcampImageId { get; set; }
    public string BootcampImagePath { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Detail { get; set; }
    public DateTime Deadline { get; set; }
}
