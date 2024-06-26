﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Instructor : User
{
    public string CompanyName { get; set; }
    
    public ICollection<Bootcamp> Bootcamps { get; set; }
    public virtual ICollection<InstructorImage> InstructorImages { get; set; }

    public Instructor()
    {
        Bootcamps = new HashSet<Bootcamp>();
        InstructorImages = new HashSet<InstructorImage>();
    }

    public Instructor(string companyName)
    {
        CompanyName = companyName;
       
    }
}
