using NArchitecture.Core.Application.Responses;

namespace Application.Features.Certificates.Commands.Create;

public class CreatedCertificateResponse : IResponse
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int BootcampId { get; set; }
}