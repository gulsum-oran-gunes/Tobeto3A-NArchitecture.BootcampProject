using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IQuizRepository : IAsyncRepository<Quiz, int>, IRepository<Quiz, int> { }
