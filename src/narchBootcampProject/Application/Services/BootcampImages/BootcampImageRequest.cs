using NArchitecture.Core.Application.Pipelines.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BootcampImages;

public class BootcampImageRequest: ICacheRemoverRequest 
{
    public int BootcampId { get; set; }
    public string ImagePath { get; set; }
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBootcampImages"];
  
}
