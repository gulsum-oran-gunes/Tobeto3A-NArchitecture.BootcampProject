using NArchitecture.Core.Application.Responses;

namespace Application.Features.BootcampContents.Commands.Create;

public class CreatedBootcampContentResponse : IResponse
{
    public int Id { get; set; }
    public int BootcampId { get; set; }
    public string? VideoUrl { get; set; }
    public string? Content { get; set; }
}