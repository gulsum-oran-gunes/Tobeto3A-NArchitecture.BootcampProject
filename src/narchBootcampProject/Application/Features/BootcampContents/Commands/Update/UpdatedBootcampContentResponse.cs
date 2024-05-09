using NArchitecture.Core.Application.Responses;

namespace Application.Features.BootcampContents.Commands.Update;

public class UpdatedBootcampContentResponse : IResponse
{
    public int Id { get; set; }
    public int BootcampId { get; set; }
    public string? VideoUrl { get; set; }
    public string? Content { get; set; }
}