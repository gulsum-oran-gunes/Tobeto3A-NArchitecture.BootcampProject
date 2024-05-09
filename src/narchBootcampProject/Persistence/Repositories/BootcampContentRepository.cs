using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BootcampContentRepository : EfRepositoryBase<BootcampContent, int, BaseDbContext>, IBootcampContentRepository
{
    public BootcampContentRepository(BaseDbContext context) : base(context)
    {
    }
}