using NArchitecture.Core.Application.Responses;

namespace Application.Features.BootcampContents.Commands.Delete;

public class DeletedBootcampContentResponse : IResponse
{
    public int Id { get; set; }
}