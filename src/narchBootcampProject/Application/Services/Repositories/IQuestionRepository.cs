using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IQuestionRepository : IAsyncRepository<Question, int>, IRepository<Question, int>
{
    public Task<List<Question>> GetAllAsync();
}
