using NArchitecture.Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BootcampVideos.Queries.GetById;
public class GetByIdBootcampVideoResponse : IResponse
{
    public int Id { get; set; }
    public int BootcampId { get; set; }
    public string ThumbnailUrl { get; set; }
}
