using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IApplicantBootcampContentRepository : IAsyncRepository<ApplicantBootcampContent, int>, IRepository<ApplicantBootcampContent, int>
{
}