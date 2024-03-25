﻿using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities;
public class Applicant : User

{
    public string About { get; set; }

    public ICollection<ApplicationEntity> ApplicationEntities { get; set; }
    public Applicant()
    {
        ApplicationEntities = new HashSet<ApplicationEntity>();
    }

    public Applicant(string about)
    {
        About = about;
    }
}