using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class BootcampState : Entity<int>
{
    public string Name { get; set; }

    public ICollection<Bootcamp> Bootcamps { get; set; }

    public BootcampState()
    {
        Bootcamps = new HashSet<Bootcamp>();
    }

    public BootcampState(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
