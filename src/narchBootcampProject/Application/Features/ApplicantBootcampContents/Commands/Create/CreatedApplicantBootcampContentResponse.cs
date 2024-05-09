using NArchitecture.Core.Application.Responses;

namespace Application.Features.ApplicantBootcampContents.Commands.Create;

public class CreatedApplicantBootcampContentResponse : IResponse
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int BootcampContentId { get; set; }
}