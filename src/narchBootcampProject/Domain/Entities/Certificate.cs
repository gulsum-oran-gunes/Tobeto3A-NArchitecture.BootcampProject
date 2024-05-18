using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Certificate: Entity<int>
{
    public Guid  ApplicantId { get; set; }
    public int BootcampId { get; set; }
    public virtual Applicant? Applicant { get; set; }
    public virtual Bootcamp? Bootcamp { get; set; }

    public Certificate()
    {
            
    }
    public Certificate(Guid applicantId, int bootcampId) : this()
    {
        ApplicantId = applicantId;
        BootcampId = bootcampId;
    }
}
