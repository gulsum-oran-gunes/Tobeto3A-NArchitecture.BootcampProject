using NArchitecture.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BootcampVideos.Queries.GetList;
public class GetListBootcampVideoListItemDto : IDto
{
    public int Id { get; set; }
    public int BootcampId { get; set; }
    public string ThumbnailUrl { get; set; }
}
