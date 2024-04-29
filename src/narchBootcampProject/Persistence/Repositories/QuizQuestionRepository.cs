using System.Linq.Expressions;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class QuizQuestionRepository : EfRepositoryBase<QuizQuestion, int, BaseDbContext>, IQuizQuestionRepository
{
    private readonly BaseDbContext _context;

    public QuizQuestionRepository(BaseDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<List<QuizQuestion>> GetAllAsync(
        Expression<Func<QuizQuestion, bool>> predicate = null,
        Func<IQueryable<QuizQuestion>, IIncludableQueryable<QuizQuestion, object>>? include = null
    )
    {
        IQueryable<QuizQuestion> queryable = Query();
        if (include != null)
            queryable = include(queryable);
        if (predicate != null)
            queryable = queryable.Where(predicate);
        return await queryable.ToListAsync();
    }
}
