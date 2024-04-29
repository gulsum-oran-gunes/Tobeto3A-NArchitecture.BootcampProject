using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Quiz : Entity<int>
{
    public Guid ApplicantId { get; set; }
    public int BootcampId { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public virtual Applicant? Applicant { get; set; }
    public virtual Bootcamp? Bootcamp { get; set; }

    [JsonIgnore]
    public ICollection<QuizQuestion> QuizQuestions { get; set; }

    public Quiz()
    {
        QuizQuestions = new HashSet<QuizQuestion>();
    }

    public Quiz(Guid applicantId, int bootcampId, DateTime? startTime, DateTime? endTime)
    {
        ApplicantId = applicantId;
        BootcampId = bootcampId;
        StartTime = startTime;
        EndTime = endTime;
    }
}
