using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.Quizs;

public interface IQuizService
{
    Task<Quiz?> GetAsync(
        Expression<Func<Quiz, bool>> predicate,
        Func<IQueryable<Quiz>, IIncludableQueryable<Quiz, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Quiz>?> GetListAsync(
        Expression<Func<Quiz, bool>>? predicate = null,
        Func<IQueryable<Quiz>, IOrderedQueryable<Quiz>>? orderBy = null,
        Func<IQueryable<Quiz>, IIncludableQueryable<Quiz, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Quiz> AddAsync(Quiz quiz);
    Task<Quiz> UpdateAsync(Quiz quiz);
    Task<Quiz> DeleteAsync(Quiz quiz, bool permanent = false);
}
