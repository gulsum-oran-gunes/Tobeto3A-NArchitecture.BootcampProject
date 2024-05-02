﻿using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
public class BootcampVideoRepository : EfRepositoryBase<BootcampVideo, int, BaseDbContext>, IBootcampVideoRepository
{
    public BootcampVideoRepository(BaseDbContext context)
        : base(context) { }
}
