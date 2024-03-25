using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Blacklist : Entity<int>
{
    public Guid ApplicantId { get; set; }

    public string Reason { get; set; }

    public DateTime Date { get; set; }
    public virtual Applicant? Applicant { get; set; }
    public Blacklist()
    {

    }
    public Blacklist(int id, Guid applicantId, string reason, DateTime date)
    {
        Id = id;
        ApplicantId = applicantId;
        Reason = reason;
        Date = date;
    }
}
