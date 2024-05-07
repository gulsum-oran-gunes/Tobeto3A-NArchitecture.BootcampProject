using NArchitecture.Core.Application.Dtos;

namespace Application.Features.ApplicationEntities.Queries.GetList;

public class GetListApplicationEntityListItemDto : IDto
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public string ApplicantFirstName { get; set; }
    public string ApplicantLastName { get; set; }
    public int BootcampId { get; set; }
    public string BootcampName { get; set; }
    public int ApplicationStateId { get; set; }
    public string ApplicationStateName { get; set; }
}
