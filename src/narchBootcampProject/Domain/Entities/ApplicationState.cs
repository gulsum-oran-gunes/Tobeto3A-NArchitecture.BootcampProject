using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities;
public class ApplicationState : Entity<int>
{

    public string Name { get; set; }

    public ICollection<ApplicationEntity> ApplicationEntities { get; set; }

    public ApplicationState()
    {
        ApplicationEntities = new HashSet<ApplicationEntity>();
    }
    public ApplicationState(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
