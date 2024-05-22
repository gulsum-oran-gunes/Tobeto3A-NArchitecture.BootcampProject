using NArchitecture.Core.Application.Dtos;

namespace Application.Features.BootcampContents.Queries.GetList;

public class GetListBootcampContentListItemDto : IDto
{
    public int Id { get; set; }
    public int BootcampId { get; set; }
    public string BootcampName { get; set; }
    public string? VideoUrl { get; set; }
    public string? Content { get; set; }
    public bool? HasApplicantBootcampContent { get; set; }
    public bool? IfApplicantPassed { get; set; }


}