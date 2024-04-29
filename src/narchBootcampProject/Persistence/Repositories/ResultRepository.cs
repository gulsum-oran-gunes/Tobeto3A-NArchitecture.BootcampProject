using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ResultRepository : EfRepositoryBase<Result, int, BaseDbContext>, IResultRepository
{
    public ResultRepository(BaseDbContext context)
        : base(context) { }
}
