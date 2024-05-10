using NArchitecture.Core.Application.Responses;

namespace Application.Features.BootcampContents.Queries.GetById;

public class GetByIdBootcampContentResponse : IResponse
{
    public int Id { get; set; }
    public int BootcampId { get; set; }
    public string BootcampName { get; set; }
    public string? VideoUrl { get; set; }
    public string? Content { get; set; }
}