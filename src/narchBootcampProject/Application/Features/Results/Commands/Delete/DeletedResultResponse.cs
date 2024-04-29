using NArchitecture.Core.Application.Responses;

namespace Application.Features.Results.Commands.Delete;

public class DeletedResultResponse : IResponse
{
    public int Id { get; set; }
}
