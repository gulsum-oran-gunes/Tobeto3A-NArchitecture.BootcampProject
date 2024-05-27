using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities;

public class Applicant : User
{
    public string? About { get; set; }
    public bool? EmailVerified { get; set; }

    public ICollection<ApplicationEntity> ApplicationEntities { get; set; }
    public ICollection<Quiz> Quizzes { get; set; }
    public ICollection<ApplicantBootcampContent>ApplicantBootcampContents { get; set; }
    public ICollection<Certificate> Certificates { get; set; }


    public Applicant()
    {
        ApplicationEntities = new HashSet<ApplicationEntity>();
        Quizzes = new HashSet<Quiz>();
        ApplicantBootcampContents = new HashSet<ApplicantBootcampContent>();
        Certificates = new HashSet<Certificate>();
    }

    public Applicant(string? about, bool? emailVerified)
    {
        About = about;
        EmailVerified = emailVerified;
    }
}
