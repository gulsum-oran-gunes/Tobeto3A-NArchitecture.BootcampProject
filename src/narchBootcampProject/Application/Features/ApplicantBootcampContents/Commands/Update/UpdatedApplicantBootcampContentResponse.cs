using NArchitecture.Core.Application.Responses;

namespace Application.Features.ApplicantBootcampContents.Commands.Update;

public class UpdatedApplicantBootcampContentResponse : IResponse
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int BootcampContentId { get; set; }
}