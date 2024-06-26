﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities;

public class Bootcamp : Entity<int>
{
    public string Name { get; set; }
    public Guid InstructorId { get; set; }
    public int BootcampStateId { get; set; }
    public string? Detail {  get; set; }
    public DateTime? Deadline {  get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public virtual ICollection<BootcampImage> BootcampImages { get; set; }
    public ICollection<ApplicationEntity> ApplicationEntities { get; set; }
    [JsonIgnore]
    public ICollection<Question> Questions { get; set; }
    [JsonIgnore]
    public ICollection<Quiz> Quizzes { get; set; }
    public ICollection<Certificate> Certificates { get; set; }

    public Bootcamp()
    {
        BootcampImages = new HashSet<BootcampImage>();
        ApplicationEntities = new HashSet<ApplicationEntity>();
        Questions = new HashSet<Question>();
        Quizzes = new HashSet<Quiz>();
        Certificates = new HashSet<Certificate>();
    }

    public virtual Instructor? Instructor { get; set; }
    public virtual BootcampState? BootcampState { get; set; }

    public Bootcamp(string name, Guid ınstructorId, int bootcampStateId, string? detail, DateTime? deadline, DateTime startDate, DateTime endDate)
    {
        Name = name;
        InstructorId = ınstructorId;
        BootcampStateId = bootcampStateId;
        Detail = detail;
        Deadline = deadline;
        StartDate = startDate;
        EndDate = endDate;
    }
}
