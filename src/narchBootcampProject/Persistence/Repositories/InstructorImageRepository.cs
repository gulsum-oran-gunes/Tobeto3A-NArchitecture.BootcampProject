using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class InstructorImageRepository : EfRepositoryBase<InstructorImage, int, BaseDbContext>, IInstructorImageRepository
{
    public InstructorImageRepository(BaseDbContext context) : base(context)
    {
    }
}