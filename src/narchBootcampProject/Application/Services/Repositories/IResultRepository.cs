using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IResultRepository : IAsyncRepository<Result, int>, IRepository<Result, int> { }
