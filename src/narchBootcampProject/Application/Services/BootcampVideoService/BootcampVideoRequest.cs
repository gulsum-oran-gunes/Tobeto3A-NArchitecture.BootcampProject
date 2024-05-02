using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BootcampVideoService;
public class BootcampVideoRequest
{
   // public int Id { get; set; }
    public int BootcampId { get; set; }
    
    public string ThumbnailUrl { get; set; }
}
