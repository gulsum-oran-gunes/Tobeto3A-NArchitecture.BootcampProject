using NArchitecture.Core.Application.Responses;

namespace Application.Features.Instructors.Commands.Delete;

public class DeletedInstructorResponse : IResponse
{
    public Guid Id { get; set; }
    public DateTime DeletedDate { get; set; }
}
