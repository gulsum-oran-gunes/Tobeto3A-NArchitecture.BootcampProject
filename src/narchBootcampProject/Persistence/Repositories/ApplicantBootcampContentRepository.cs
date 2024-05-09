using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ApplicantBootcampContentRepository : EfRepositoryBase<ApplicantBootcampContent, int, BaseDbContext>, IApplicantBootcampContentRepository
{
    public ApplicantBootcampContentRepository(BaseDbContext context) : base(context)
    {
    }
}