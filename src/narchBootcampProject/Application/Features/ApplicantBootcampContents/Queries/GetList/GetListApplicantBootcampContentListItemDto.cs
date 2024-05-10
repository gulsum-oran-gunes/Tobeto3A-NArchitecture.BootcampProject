using NArchitecture.Core.Application.Dtos;

namespace Application.Features.ApplicantBootcampContents.Queries.GetList;

public class GetListApplicantBootcampContentListItemDto : IDto
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public string ApplicantFirstName { get; set; }
    public string ApplicantLastName { get; set; }
    public int BootcampContentId { get; set; }
    public string BootcampContentVideoUrl { get; set; }
    public string BootcampContentContent { get; set; }

}