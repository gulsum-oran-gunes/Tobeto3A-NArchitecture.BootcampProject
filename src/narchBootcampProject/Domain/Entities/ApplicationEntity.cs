﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class ApplicationEntity : Entity<int>
{
    public Guid ApplicantId { get; set; }
    public int BootcampId { get; set; }
    public int ApplicationStateId { get; set; } = 1;

    public virtual Bootcamp? Bootcamp { get; set; }
    public virtual Applicant? Applicant { get; set; }

    public virtual ApplicationState? ApplicationState { get; set; }

    public ApplicationEntity() { }

    public ApplicationEntity(int id, Guid applicantId, int bootcampId, int applicationStateId)
    {
        Id = id;
        ApplicantId = applicantId;
        BootcampId = bootcampId;
        ApplicationStateId = applicationStateId;
    }
}
