using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities;
public class Bootcamp : Entity<int>
{
    public string Name { get; set; }
    public Guid InstructorId { get; set; }
    public int BootcampStateId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public virtual ICollection<BootcampImage> BootcampImages { get; set; }
    public ICollection<ApplicationEntity> ApplicationEntities { get; set; }
    public Bootcamp()
    {
        BootcampImages = new HashSet<BootcampImage>();
        ApplicationEntities = new HashSet<ApplicationEntity>();
    }
    public virtual Instructor? Instructor { get; set; }
    public virtual BootcampState? BootcampState { get; set; }


    public Bootcamp(int id, string name, Guid instructorId, int bootcampState, DateTime startDate, DateTime endDate)
    {
        Id = id;
        Name = name;
        InstructorId = instructorId;
        BootcampStateId = bootcampState;
        StartDate = startDate;
        EndDate = endDate;
    }
}
