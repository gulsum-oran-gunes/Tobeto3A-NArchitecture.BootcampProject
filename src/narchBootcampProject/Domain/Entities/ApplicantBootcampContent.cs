using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class ApplicantBootcampContent: Entity<int>
{
    public Guid ApplicantId { get; set; }
    public int BootcampContentId { get; set; }
    public virtual Applicant? Applicant { get; set; }
    public virtual BootcampContent? BootcampContent { get; set; }
}
