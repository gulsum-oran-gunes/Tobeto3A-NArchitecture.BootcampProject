using NArchitecture.Core.Application.Dtos;

namespace Application.Features.ApplicantBootcampContents.Queries.GetList;

public class GetListApplicantBootcampContentListItemDto : IDto
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int BootcampContentId { get; set; }
}