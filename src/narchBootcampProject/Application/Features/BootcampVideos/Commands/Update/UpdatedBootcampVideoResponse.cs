using NArchitecture.Core.Application.Responses;

namespace Application.Features.BootcampVideos.Commands.Update;

public class UpdatedBootcampVideoResponse : IResponse
{
    public int Id { get; set; }
    public int BootcampId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ThumbnailUrl { get; set; }
}