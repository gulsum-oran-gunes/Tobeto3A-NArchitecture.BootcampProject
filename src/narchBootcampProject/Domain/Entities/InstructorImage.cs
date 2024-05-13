using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class InstructorImage: Entity<int>
{
    public Guid InstructorId { get; set; }
    public string ImagePath { get; set; }
    public virtual Instructor? Instructor { get; set; }

    public InstructorImage()
    {
        
    }

    public InstructorImage(int id, Guid ınstructorId, string ımagePath)
    {
        Id = id;
        InstructorId = ınstructorId;
        ImagePath = ımagePath;
    }

}
