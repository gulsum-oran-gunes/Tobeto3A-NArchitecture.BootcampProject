using NArchitecture.Core.Application.Responses;

namespace Application.Features.ApplicantBootcampContents.Commands.Delete;

public class DeletedApplicantBootcampContentResponse : IResponse
{
    public int Id { get; set; }
}