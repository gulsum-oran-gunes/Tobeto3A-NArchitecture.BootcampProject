using NArchitecture.Core.Application.Responses;

namespace Application.Features.ApplicantBootcampContents.Queries.GetById;

public class GetByIdApplicantBootcampContentResponse : IResponse
{
    public int Id { get; set; }
    public Guid ApplicantId { get; set; }
    public int BootcampContentId { get; set; }
}