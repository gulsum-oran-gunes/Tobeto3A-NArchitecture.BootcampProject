﻿using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Employee : User
{
    public string Position { get; set; }
    public Employee()
    {

    }

    public Employee(string position)
    {

        Position = position;
    }
}
