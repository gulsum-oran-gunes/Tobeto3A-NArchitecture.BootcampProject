using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Certificates.Queries.GetList;

public class GetListCertificateListItemDto : IDto
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int BootcampId { get; set; }
    public string BootcampName { get; set; }
    public DateTime CreatedDate { get; set; }
}