using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BootcampImages;

public class BootcampImageRequest
{
    public int BootcampId { get; set; }
    public string ImagePath { get; set; }
}
