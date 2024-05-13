using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.InstructorImages;
public class UpdateInstructorImageRequest
{
    public int Id { get; set; }
    public Guid InstructorId { get; set; }
}
