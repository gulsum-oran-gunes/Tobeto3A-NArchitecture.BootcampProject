using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IBootcampContentRepository : IAsyncRepository<BootcampContent, int>, IRepository<BootcampContent, int>
{
}