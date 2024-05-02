using NArchitecture.Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BootcampVideos.Commands.Delete;
public class DeletedBootcampVideoResponse : IResponse
{
    public int Id { get; set; }
}