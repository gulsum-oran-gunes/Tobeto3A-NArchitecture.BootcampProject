using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IQuizQuestionRepository : IAsyncRepository<QuizQuestion, int>, IRepository<QuizQuestion, int>
{
    public Task<List<QuizQuestion>> GetAllAsync(
        Expression<Func<QuizQuestion, bool>> predicate = null,
        Func<IQueryable<QuizQuestion>, IIncludableQueryable<QuizQuestion, object>>? include = null
    );
}
