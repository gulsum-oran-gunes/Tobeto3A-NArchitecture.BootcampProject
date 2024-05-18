using NArchitecture.Core.Application.Responses;

namespace Application.Features.Certificates.Queries.GetById;

public class GetByIdCertificateResponse : IResponse
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int BootcampId { get; set; }
}