using NArchitecture.Core.Application.Responses;

namespace Application.Features.InstructorImages.Commands.Delete;

public class DeletedInstructorImageResponse : IResponse
{
    public int Id { get; set; }
}